using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.core.Entitys.Order_Aggregate
{
    [NotMapped]
    public class Address
    {
        public int Id { get; set; } 
        public string FirastName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string  Street { get; set; }
        public string Country { get; set; }

        public string AppUserId { get; set; } 
        public AppUser appUser { get; set; }




    }
}
