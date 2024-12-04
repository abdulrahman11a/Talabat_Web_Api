using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value = "Payment Failed")]

        PaymentFailed,
        [EnumMember(Value = "Cancelled")]
        Cancelled


    }
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        CashOnDelivery,
        BankTransfer
    }

}
