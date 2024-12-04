using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entitys.Order_Aggregate
{
    [NotMapped]

    public class AppUser:IdentityUser
    {
        public string DisplayName { get; set; } 

        public Address address { get; set; }    



    }
}
