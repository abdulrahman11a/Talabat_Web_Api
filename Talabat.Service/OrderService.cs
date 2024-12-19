using Talabat.core.Entites;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;
using Talabat.core.Repositories.Contract;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications.Order_Spec;

namespace Talabat.Applacation
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaxRegion _taxRegion;
        private readonly IDiscount _discount;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork , ITaxRegion taxRegion,IDiscount discount,IPaymentService paymentService)
        {
            this._basketRepo = basketRepo;
            _unitOfWork = unitOfWork;   
            this._taxRegion = taxRegion;
            this._discount = discount;
            this._paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(
        string buyerEmail,
        string basketId,
        int deliveryMethodId,
        Address address,
        Discount discountType,
        bool isMember = false)
        {
            // 1. Retrieve the basket
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null || !basket.Items.Any())
            {
                throw new Exception("Basket is empty or invalid.");
            }


            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                var repo = _unitOfWork.repository<Product>();
            foreach (var item in basket.Items)
            {
                var product = await repo.GetByIdAsync(item.Id);
                if (product == null) throw new Exception($"Product with ID {item.Id} not found.");

                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            }
            var orderRepo = _unitOfWork.repository<Order>();

            // 3. Calculate subtotal
            var subtotal = orderItems.Sum(o => o.Price * o.Quantity);

            // 4. Retrieve the delivery method
            var deliveryMethod = await _unitOfWork.repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
            if (deliveryMethod == null) throw new Exception($"Delivery method with ID {deliveryMethodId} not found.");


            // 6. Apply discount
            var discountService = new DiscountService(); // Replace with DI if needed
            var discountRate = _discount.ApplyDiscount(discountType, subtotal, isMember);
            var discountAmount = subtotal * discountRate;

            // 5. Calculate tax
            var taxAmount=0m;
            if (Enum.TryParse<TaxRegion>(address.City, true, out var taxRegion))
            {
                taxAmount = _taxRegion.CalculateTax(subtotal, taxRegion);
            }
           var orderforPaymetSpecification=new OrderBaseSpecification();
            orderforPaymetSpecification.UpdateCriteriaForPaymentIntentId(basket.PaymentIntentId);
            var EntityWithSpec=await orderRepo.GetEntityWithSpecAsync(orderforPaymetSpecification);

            if(EntityWithSpec is { })
            {
                orderRepo.Delete(EntityWithSpec);

               await _paymentService.CreateOrUpdatePaymentIntentAsync(basket.PaymentIntentId);
            }


            // 8. Create the order
            var order = new Order(
                buyerEmail: buyerEmail,
                shippingAddress: address,
                discount: discountAmount,
                deliveryMethod: deliveryMethod,
                items: orderItems,
                tax: taxAmount, 
                subTotal: subtotal,
                _paymentInterntId: basket.PaymentIntentId
            );
           await orderRepo.AddAsync(order);

          var res=   await _unitOfWork.CompleteAsync();

            return res>0? order:null;
           

        }


        public async Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string buyerEmail)
        {
            var repo = _unitOfWork.repository<Order>();

            var spec = new OrderBaseSpecification(buyerEmail);

               var Order=await repo.GetAllWithSpecAsync(spec);

            return Order == null ? null : Order;



        }

        public async Task<Order?> GetOrderByIdForUserAsync(int OrderId, string buyerEmail)
        {

            var repo = _unitOfWork.repository<Order>();

            var spec = new OrderBaseSpecification(OrderId, buyerEmail);

            var Order = await repo.GetEntityWithSpecAsync(spec);

            return Order == null ? null : Order;
        }

        public async Task<IReadOnlyList<DeliveryMethod?>> GetDeliveryMethodsAsync()
          => await _unitOfWork.repository<DeliveryMethod>().GetAllAsync();

    
    }
}
