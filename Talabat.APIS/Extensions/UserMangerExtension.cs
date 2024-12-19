using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.core.Entitys.Identity;

namespace Talabat.APIS.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser> FindUserWithAddressByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            try
            {
                return await userManager.Users
                    .Include(u => u.address)
                    .SingleOrDefaultAsync(u => u.Email == email)
                    ?? throw new InvalidOperationException("User not found.");
            }
            catch (Exception ex)
            {
                // Log the error
                throw new Exception($"Error fetching user with email {email}: {ex.Message}", ex);
            }

        }




    }
}
