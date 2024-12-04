using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites.Common;

namespace Talabat.core.Entitys.Order_Aggregate
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrdered productItemOrdered, decimal price, int quantity)
        {
            ProductItemOrdered = productItemOrdered;
            Price = price;
            Quantity = quantity;
        }


        public ProductItemOrdered ProductItemOrdered { get; set; }
        public decimal Price { get; set; }  
        public int  Quantity { get; set;}



    }
}
