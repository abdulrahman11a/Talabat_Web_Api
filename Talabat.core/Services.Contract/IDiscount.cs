using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.core.Services.Contract
{
    public interface IDiscount
    {
        public decimal ApplyDiscount(Discount discount, decimal subTotal, bool isMember = false);


    }
}
