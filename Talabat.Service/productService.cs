using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites;
using Talabat.core.Repositories.Contract;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications.Product_Spec;

namespace Talabat.Applacation
{
    public class productService(IUnitOfWork _unitOfWork): IproductService
    {

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecparams specparams)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(specparams);

           var products = await _unitOfWork.repository<Product>().GetAllWithSpecAsync(spec);
            
            return products;    


        }



        public async Task<Product> GetProductAsync(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);
            var product = await _unitOfWork.repository<Product>().GetEntityWithSpecAsync(spec);

            return product;
        }


        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
           => await _unitOfWork.repository<ProductBrand>().GetAllAsync();


        public async Task<IReadOnlyList<ProductType>> GetTypesAsync()
           => await _unitOfWork.repository<ProductType>().GetAllAsync();

        public async Task<int> GetCountAsync(ProductSpecparams specparams)
        {
            var specCount = new ProductWithFiltrationForCountSpecification(specparams);

            return await _unitOfWork.repository<Product>().GetCountAsync(specCount);
        }
    }
}
