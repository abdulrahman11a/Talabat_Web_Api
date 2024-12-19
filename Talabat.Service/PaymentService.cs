using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites;
using Talabat.core.Entitys;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Repositories.Contract;
using Product = Talabat.core.Entites.Product;

namespace Talabat.Applacation
{
    public class PaymentService : IPaymentService
    {
        public PaymentService(IConfiguration configuration, IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripKey:Secretkey"];
            var basket = await _basketRepo.GetBasketAsync(BasketId);


            if (basket == null) return null;

            var ShippingPrice = 0m;
            #region Calclulate DeliveryMethod And Update ShippingPrice If exist
            if (basket.DeliveryMethodId.HasValue)
            {
                var Delivery = await _unitOfWork.repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                if (Delivery is { })
                    ShippingPrice = Delivery.Cost;
            }
            #endregion
            
            #region Calculate basket And Update in case Changed Price
            if (basket.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.repository<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;

                }

            } 
            #endregion

            PaymentIntentService paymentService = new();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                PaymentIntentCreateOptions createOptions = new()
                {
                    Amount = (long)basket.Items.Sum(i => i.Price * i.Quantity * 100) + (long)(ShippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                var paymentIntent = await paymentService.CreateAsync(createOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                try
                {
                    PaymentIntentUpdateOptions updateOptions = new()
                    {
                        Amount = (long)basket.Items.Sum(i => i.Price * i.Quantity * 100) + (long)(ShippingPrice * 100),
                    };

                    await paymentService.UpdateAsync(basket.PaymentIntentId, updateOptions);
                }
                catch (StripeException ex)
                {
                    Console.WriteLine($"Stripe API Error: {ex.StripeError.Message}");
                    throw;
                }
            }

            await _basketRepo.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
