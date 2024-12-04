using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.core.Services.Contract
{
    public interface IToken
    {
        public Task<string>CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);



    }
}
