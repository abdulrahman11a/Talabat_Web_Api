using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entitys;

namespace Talabat.core.Repositories.Contract
{
    public interface  IBasketRepository
    {

        Task<CustomerBasket?>GetBasketAsync(string BasketId);

        Task<CustomerBasket?>UpdateBasketAsync(CustomerBasket basket);

         Task<bool> DeleteAllAsync(string BasketId);



    }
}
