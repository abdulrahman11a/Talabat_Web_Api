using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entitys.Identity;

namespace Talabat.Infrastructure.Identity.Data
{
    public class AppIdentitystoreDbcontxt:IdentityDbContext<AppUser>
    {
        public AppIdentitystoreDbcontxt(DbContextOptions<AppIdentitystoreDbcontxt> options):base(options) { }

    }
}
