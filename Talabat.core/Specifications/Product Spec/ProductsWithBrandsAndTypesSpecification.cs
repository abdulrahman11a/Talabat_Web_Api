using System.Linq;
using Talabat.core.Entites;

namespace Talabat.core.Specifications.Product_Spec
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<Product>
    {
        public ProductsWithBrandsAndTypesSpecification(ProductSpecparams specparams)
            : base(p =>
                (string.IsNullOrEmpty(specparams.Search) || p.Name.ToLower().Contains(specparams.Search)) &&
                (!specparams.BrandId.HasValue || p.ProductBrandId == specparams.BrandId) &&
                (!specparams.TypeId.HasValue || p.ProductTypeId == specparams.TypeId)
            )
        {
            switch (specparams.Sort)
            {
                case "NameAsc":
                    ApplyOrderBy(x => x.Name); // Ascending by Name
                    break;
                case "NameDesc":
                    ApplyOrderByDescending(x => x.Name); // Descending by Name
                    break;
                case "PriceAsc":
                    ApplyOrderBy(x => x.Price); // Ascending by Price
                    break;
                case "PriceDesc":
                    ApplyOrderByDescending(x => x.Price); // Descending by Price
                    break;
                default:
                    ApplyOrderBy(x => x.Name); // Default sort by Name ascending
                    break;
            }

            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);

            ApplyPagination(specparams.PageSize, (specparams.PageIndex - 1) * specparams.PageSize);
        }

        public ProductsWithBrandsAndTypesSpecification(int id)
            : base(x => x.Id == id)
        {
            Includes.Add(x => x.ProductBrand);
            Includes.Add(x => x.ProductType);
        }
    }
}
