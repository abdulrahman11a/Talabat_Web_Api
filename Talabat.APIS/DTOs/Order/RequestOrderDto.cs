using System.ComponentModel.DataAnnotations; // For data annotations
using Talabat.APIS.DTOs.Shared;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.APIS.DTOs.Order
{
    public class RequestOrderDto
    {
        [Required]
        public string BasketId { get; set; }

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public AddressDto Address { get; set; }

        public bool IsMember { get; set; }
    }
}
