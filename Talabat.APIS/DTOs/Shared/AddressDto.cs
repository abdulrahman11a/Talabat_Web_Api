using System.ComponentModel.DataAnnotations;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.APIS.DTOs.Shared
{
    public class AddressDto
    {
        [Required]
        public string FirastName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Street { get; set; }
        [Required]

        public string Country { get; set; }

    }
}
