using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites;
using Talabat.core.Specifications.Product_Spec;

namespace Talabat.core.Services.Contract
{
    public interface IproductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecparams specparams); 
        Task<Product> GetProductAsync(int id); 
        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetTypesAsync();
        Task<int> GetCountAsync(ProductSpecparams specparams);




    }
}
