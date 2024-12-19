using Microsoft.AspNetCore.Identity;
using Talabat.core.Entitys.Identity;
using Talabat.core.IRepository;

namespace Talabat.Infrastructure.Identity.Data
{
    public static  class AppIdentitystoreDbcontxtSeed
    {
        public static void SeedAppUser(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {

                AppUser user1 = new()
                {
                    DisplayName = "Abdo",
                    Email = "Abdo@gmail.com",
                    UserName = "AA",
                    PhoneNumber = "10500655"
                };
                userManager.CreateAsync(user1);
                AppUser user2 = new()
                {
                    DisplayName = "Mahmed",
                    Email = "Abdo@gmail.com",
                    UserName = "AA",
                    PhoneNumber = "10500655"

                };
                userManager.CreateAsync(user2);

                AppUser user3 = new()
                {
                    DisplayName = "Rana",
                    Email = "Abdo@gmail.com",
                    UserName = "AA",
                    PhoneNumber = "10500655"

                };
                userManager.CreateAsync(user3);

                AppUser user4 = new()
                {
                    DisplayName = "Ahmed",
                    Email = "Abdo@gmail.com",
                    UserName = "AA",
                    PhoneNumber = "10500655"

                };
                userManager.CreateAsync(user4);

            }

        }



    }
}
