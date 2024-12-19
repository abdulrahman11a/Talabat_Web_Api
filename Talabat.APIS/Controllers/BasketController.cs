using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs.NewFolder;
using Talabat.APIS.Errors;
using Talabat.core.Entitys;
using Talabat.core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{
    public class BasketController(IBasketRepository basket,IMapper _mapper): ApiBaseController
    {
        //Get => Recreate OR Create

        [HttpGet("{id}") ]   
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
           var baket= await basket.GetBasketAsync(id);
            return Ok(baket?? new CustomerBasket(id));//this mean if Null time of Basket is end will ReCreate

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateCustomerBasket(CustomerBasketDto customerBasket)
        {
            var customerbasket = _mapper.Map<CustomerBasket>(customerBasket);
            var baketUpdateOrCreate= await basket.UpdateBasketAsync(customerbasket);
            var Dto=_mapper.Map<CustomerBasketDto>(baketUpdateOrCreate);
            return baketUpdateOrCreate is null ? BadRequest (new ApiResponse(400)):Ok(Dto);

        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCustomerBasket(string customerBasketID)
        {
            return await basket.DeleteAllAsync(customerBasketID);
        }




    }
}
