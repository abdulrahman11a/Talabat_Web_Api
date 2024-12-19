using System.ComponentModel.DataAnnotations;
using Talabat.core.Entitys;

namespace Talabat.APIS.DTOs.NewFolder
{
    public class CustomerBasketDto
    {
               [Required]
            public string Id { get; set; }
            public List<BasketItemDto> Items { get; set; }
            public int? DeliveryMethodId { get; set; }
            public decimal ShippingPrice { get; set; }
            public string? ClientSecret { get; set; }
            public string? PaymentIntentId { get; set; }

        }
    }

