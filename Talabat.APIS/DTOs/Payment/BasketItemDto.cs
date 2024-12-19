using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs.NewFolder
{
    public class BasketItemDto
    {

        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
       
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


    }
}
