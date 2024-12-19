using Talabat.APIS.DTOs.NewFolder;
using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.APIS.DTOs.Order
{
    public class ResponseOrderDto
    {


        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string orderStatus { get; set; }
        public Address ShippingAddress { get; set; }

        public string Currency { get; set; }

        public decimal Tax { get; set; }

        public decimal Discount { get; set; }
        public string DeliveryMethodName { get; set; }
        public decimal DeliveryMethodCost { get; set; }


        public ICollection<OrderItemDto> Items { get; set; }
        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }


        public string paymentInterntId { get; set; } = string.Empty;


    }
}