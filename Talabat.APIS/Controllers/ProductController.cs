using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Controllers;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.APIS.Helper;
using Talabat.core.Entites;
using Talabat.core.IRepository;
using Talabat.core.Specifications.Product_Spec;
public class ProductController : ApiBaseController
{
    private readonly IGenaricRepository<Product> _genaric_Product;
    private readonly IGenaricRepository<ProductType> _genaric_ProductType;
    private readonly IGenaricRepository<ProductBrand> _genaric_ProductBrand;
    private readonly IMapper _mapper;

    public ProductController(IGenaricRepository<Product> genaric_Product,
                             IGenaricRepository<ProductType> genaric_ProductType,
                             IGenaricRepository<ProductBrand> genaric_ProductBrand,
                             IMapper mapper)
    {
        _genaric_Product = genaric_Product;
        _genaric_ProductType = genaric_ProductType;
        _genaric_ProductBrand = genaric_ProductBrand;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductReturnDto), statusCode: 200)]
    [ProducesResponseType(typeof(ApiResponse), statusCode: 404)]
    public async Task<ActionResult<Pagination<ProductReturnDto>>> GetProductsSpec([FromQuery] ProductSpecparams specparams)
    {
        var spec = new ProductsWithBrandsAndTypesSpecification(specparams);

        var products = await _genaric_Product.GetAllWithSpec(spec);

        var specCount = new ProductWithFiltrationForCountApecification(specparams);
        var productsCount = await _genaric_Product.GetCountAsync(specCount);

        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDto>>(products);

        var pagination = new Pagination<ProductReturnDto>(specparams.PageIndex, specparams.PageSize, productsCount, data);

        return Ok(pagination);
    }

    [HttpGet("{ID}")]
    [ProducesResponseType(typeof(ProductReturnDto), statusCode: 200)]
    [ProducesResponseType(typeof(ApiResponse), statusCode: 404)]
    public async Task<ActionResult<ProductReturnDto>> GetProduct(int ID)
    {
        var spec = new ProductsWithBrandsAndTypesSpecification(ID);
        var product = await _genaric_Product.GetEntityWithSpec(spec);

        return product != null ? Ok(_mapper.Map<ProductReturnDto>(product)) : NotFound();
    }

    [Authorize ]
    [HttpGet("brands")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
    {
        var brands = await _genaric_ProductBrand.GetAllAsync();
        return brands.Any() ? Ok(brands) : NotFound();
    }

    [HttpGet("Types")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductType>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), statusCode: 404)]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
    {
        var types = await _genaric_ProductType.GetAllAsync();
        return types.Any() ? Ok(types) : NotFound();
    }
}
