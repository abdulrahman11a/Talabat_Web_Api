using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Data;
using System.Text;
using System.Text.Json;
using Talabat.APIS.Errors;
using Talabat.APIS.Helper;
using Talabat.Applacation;
using Talabat.core.Entitys.Identity;
using Talabat.core.IRepository;
using Talabat.core.Repositories.Contract;
using Talabat.core.Services.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure.Identity.Data;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;

namespace Talabat.APIS
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region configuring services container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbcontext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>//options for confiqure More options
            {
                var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(redisConnectionString);

            });
            builder.Services.AddDbContext<AppIdentitystoreDbcontxt>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });

           








            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            builder.Services.AddScoped(typeof(ITaxRegion), typeof(TaxRegionService));
            builder.Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            
             builder.Services.AddScoped(typeof (IproductService),typeof(productService));

            builder.Services.AddScoped(typeof(IDiscount), typeof(DiscountService));
            builder.Services.AddScoped(typeof(IToken), typeof(TokenServices));
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
           builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));//{{ not need inject GenaricRepository if use ==>UnitOfWork because I'm create manual in class UnitOfWork}}
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            builder.Services.AddIdentity<AppUser, IdentityRole>()//this Add 3 types  UserManager<AppUser>, RoleManager<IdentityRole>, and SignInManager<AppUser>.
                .AddEntityFrameworkStores<AppIdentitystoreDbcontxt>();//Internally, this adds implementations for interfaces like:
                                                                      //IUserStore<AppUser>: To manage user persistence.IRoleStore<IdentityRole>:
                                                                      //To manage role persistence.
            builder.Services.AddAutoMapper(typeof(MappProfile));
            builder.Services.AddCors(Options =>
            {


                Options.AddPolicy("MyPolicy", options =>
                {

                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.WithOrigins(builder.Configuration["frontBasUrLPolicy"]);

                });


            });
            #region Response evrye end point if Model stae invalid
            builder.Services.Configure<ApiBehaviorOptions>(op =>
          {
              op.InvalidModelStateResponseFactory = context =>
              {


                  var Errors = context.ModelState.Where(e => e.Value.Errors.Count > 0)
                  .SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();


                  var response = new ApiValadationErorr() { errors = Errors };


                  return new BadRequestObjectResult(response);
              };
          });

            #endregion



            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//   





            })

               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = false;

                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       ValidIssuer = builder.Configuration["JWT:issuer"],
                       ValidAudience = builder.Configuration["JWT:audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                       ClockSkew = TimeSpan.FromDays(double.Parse(builder.Configuration["JWT:exp"]))
                   };
               });





            #endregion
            var app = builder.Build();
            #region Update Database Explicitly


            using var scope = app.Services.CreateScope(); //Group all LifeTime Services scope
            var service = scope.ServiceProvider; //Services At Self
                var LogerFactory = service.GetRequiredService<ILoggerFactory>();
                var Loger = LogerFactory.CreateLogger<Program>();
            try
            {
                var dbcontext = service.GetRequiredService<StoreDbcontext>(); //spacific  Services is Dbcontext
                var UserManger = service.GetRequiredService<UserManager<AppUser>>();
                AppIdentitystoreDbcontxtSeed.SeedAppUser(UserManger);

                await dbcontext.Database.MigrateAsync();

                var Identiydbcontext = service.GetRequiredService<AppIdentitystoreDbcontxt>();

                await Identiydbcontext.Database.MigrateAsync();

               await StoreContextDataSeed.DataSeedAsync(dbcontext);
            }


            catch (Exception ex) 
            {

                Loger.LogError(ex, "Update DataBase");
                //Loger.LogError(ex);

            }

            #endregion
            #region Configure the HTTP request pipeline.

            #region Create Middleware 3 ways
            app.Use(async (context, _next) =>
            {

                try
                {
                    // Call the next middleware in the pipeline
                    await _next.Invoke(context);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Loger.LogError(ex, "An unhandled exception has occurred.");

                    // Set the response content type
                    context.Response.ContentType = "application/json";

                    // Create a response object with error details
                    var response = new ExceptionServerHandling()
                    {
                        Details = app.Environment.IsDevelopment() ? ex.StackTrace : null // Include stack trace only in development
                    };

                    // Write the response
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });//1-Request Delegete


            //app.UseMiddleware<ServerSideValidationMiddleware>();//2-By Convention
            // 3- FactoryBased 
            #endregion
            if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
         //   app.UseStatusCodePagesWithRedirects("/Error/{0}");//this Send Two Request first go to Action not found secunde redirect Controller
            app.UseStatusCodePagesWithReExecute("/Error/{0}");// this Send one Request  ReExecute is not  found Action secunde redirect
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseRouting();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
                #endregion
            }
        }
    }
