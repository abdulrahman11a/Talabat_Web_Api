using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.core.Entitys;
using Talabat.core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{

    public class BasketController(IBasketRepository basket): ApiBaseController
    {
        //Get => Recreate OR Create

        [HttpGet]   
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            if (BasketId is null) return BadRequest(new ApiResponse(400, "INVALID"));
           var baket= await basket.GetBasketAsync(BasketId);
            if (baket == null) return new CustomerBasket(BasketId);//this mean if Null time of Basket is end will ReCreate
            return Ok(baket);

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateCustomerBasket(CustomerBasket customerBasket)
        {
           var baketUpdateOrCreate= await basket.UpdateBasketAsync(customerBasket);   

            return baketUpdateOrCreate is null ? BadRequest (new ApiResponse(400)):Ok(baketUpdateOrCreate);

        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCustomerBasket(string customerBasketID)
        {
            return await basket.DeleteAllAsync(customerBasketID);
        }




    }
}
