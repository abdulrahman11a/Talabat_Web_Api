using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Talabat.core.Entites;
using Talabat.core.Entitys;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;
using Talabat.core.IRepository;
using Talabat.core.Repositories.Contract;
using Talabat.core.Services.Contract;

namespace Talabat.Applacation
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IGenaricRepository<Product> _productRepo;
        private readonly IGenaricRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly ITaxRegion _taxRegion;
        private readonly IDiscount _discount;

        public OrderServices(IBasketRepository basketRepo, IGenaricRepository<Product> ProductRepo, IGenaricRepository<DeliveryMethod> DeliveryMethodRepo,ITaxRegion taxRegion,IDiscount discount)
        {
            this._basketRepo = basketRepo;
            _productRepo = ProductRepo;
           _deliveryMethodRepo = DeliveryMethodRepo;
            this._taxRegion = taxRegion;
            this._discount = discount;
        }

        public async Task<Order> CreateOrderAsync(
        string buyerEmail,
        string basketId,
        int deliveryMethodId,
        Address address,
        TaxRegion taxRegion,
        Discount discountType,
        bool isMember = false)
        {
            // 1. Retrieve the basket
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null || !basket.Items.Any())
            {
                throw new Exception("Basket is empty or invalid.");
            }

            var OrderItems = new List<OrderItem>();

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.Id);
                if (product == null) throw new Exception($"Product with ID {item.Id} not found.");

                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            // 3. Calculate subtotal
            var subtotal = orderItems.Sum(o => o.Price * o.Quantity);

            // 4. Retrieve the delivery method
            var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(deliveryMethodId);
            if (deliveryMethod == null) throw new Exception($"Delivery method with ID {deliveryMethodId} not found.");

            // 5. Apply tax
            var taxRate = _taxRegion.GetTaxRate(taxRegion); // Assuming TaxRegionService is static or injected
            var taxAmount = subtotal * taxRate;

            // 6. Apply discount
            var discountService = new DiscountService(); // Replace with DI if needed
            var discountRate = _discount.ApplyDiscount(discountType, subtotal, isMember);
            var discountAmount = subtotal * discountRate;

            // 7. Calculate the total
            var totalBeforeDelivery = subtotal + taxAmount - discountAmount;
            var total = totalBeforeDelivery + (deliveryMethod?.Cost ?? 0);

            // 8. Create the order
            var order = new Order(
                buyerEmail: buyerEmail,
                shippingAddress: address,
                tax: taxAmount,
                discount: discountAmount,
                deliveryMethod: deliveryMethod,
                items: orderItems,
                subTotal: subtotal
            );

            return order;


        }



        public Task<Order> GetOrderByIdForUserAsync(int OrderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
