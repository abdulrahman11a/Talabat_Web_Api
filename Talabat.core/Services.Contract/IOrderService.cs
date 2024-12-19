using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.core.Services.Contract
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string buyerEmail, string basketId,int deliveryMethodId,
                     Address address, Discount discountType, bool isMember = false);
        Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order?> GetOrderByIdForUserAsync(int OrderId,string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod?>> GetDeliveryMethodsAsync();

    }
}
