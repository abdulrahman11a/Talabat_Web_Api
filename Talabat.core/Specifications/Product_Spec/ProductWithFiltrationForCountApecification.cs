using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entites;

namespace Talabat.core.Specifications.Product_Spec
{
    public class ProductWithFiltrationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltrationForCountSpecification(ProductSpecparams specparams)
             : base(p =>
               (string.IsNullOrEmpty(specparams.Search) || p.Name.ToLower().Contains(specparams.Search)) &&
                (!specparams.BrandId.HasValue || p.ProductBrandId == specparams.BrandId) &&
                (!specparams.TypeId.HasValue || p.ProductTypeId == specparams.TypeId))
        {



        }
    }
}
