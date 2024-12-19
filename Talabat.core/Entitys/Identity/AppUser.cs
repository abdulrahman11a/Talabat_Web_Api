using Microsoft.AspNetCore.Identity;

namespace Talabat.core.Entitys.Identity
{

    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        // [JsonIgnore]//for  cyclic
        public Address address { get; set; }



    }
}
