using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.core.Entitys.Identity;
using Talabat.core.Services;
using Talabat.core.Services.Contract;

namespace Talabat.Applacation
{
    public class TokenServices(IConfiguration _configuration) : IToken
    {

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {

            #region Claims
            var AuthToken = new List<Claim>() {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                //new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            }; 
            #endregion

            #region Role
            var AuthRole = await userManager.GetRolesAsync(user);
            foreach (var role in AuthRole)
                AuthToken.Add(new Claim(ClaimTypes.Role, role)); 
            #endregion

            #region Key

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); // Sets alg: "HS256"
            #endregion



            #region TOKEN
            JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:issuer"],
            audience: _configuration["JWT:audience"],
            claims: AuthToken,
           expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:exp"])),
           signingCredentials: credentials
            ); 
            #endregion

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
