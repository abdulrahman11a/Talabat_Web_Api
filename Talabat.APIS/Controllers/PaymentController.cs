using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.core.Entitys;
using Talabat.core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{

    public class PaymentController(IPaymentService _paymentService):ApiBaseController
    {

        [HttpPost("BasketId")]
        public async Task<ActionResult<CustomerBasket?>> CreateOrUpdatPayment(string BasketId)
        {
           var basket= await _paymentService.CreateOrUpdatePaymentIntentAsync(BasketId);

            return basket is { } ? Ok(basket) : BadRequest(new ApiResponse(400, "basket not exist id "));
        }
    }
}
