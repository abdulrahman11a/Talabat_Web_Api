using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites.Common;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.core.Entitys.Order_Aggregate
{
    public  class Order:BaseEntity 
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, decimal tax, decimal discount, 
                    DeliveryMethod deliveryMethod, ICollection<OrderItem> items,decimal subTotal, string currency = "USD")
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            Currency = currency;
            Tax = tax;
            Discount = discount;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; } 
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus orderStatus { get; set; }
        public Address ShippingAddress { get; set; }

        public string Currency { get; set; } = "USD";

        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public DeliveryMethod DeliveryMethod  { get; set; }
        public  ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }
     
        public decimal GetTotal() => (DeliveryMethod?.Cost ?? 0) + SubTotal + Tax - Discount;

        public decimal GetTotalAfterDiscount() => GetTotal() - Discount;

        public string paymentInterntId { get; set; } =string.Empty;






    }
}





