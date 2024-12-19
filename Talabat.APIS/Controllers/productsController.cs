using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Controllers;
using Talabat.APIS.DTOs.ProductDto;
using Talabat.APIS.Errors;
using Talabat.APIS.Helper;
using Talabat.core.Entites;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications.Product_Spec;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Controllers;
using Talabat.APIS.DTOs.ProductDto;
using Talabat.APIS.Errors;
using Talabat.APIS.Helper;
using Talabat.core.Entites;
using Talabat.core.Services.Contract;
using Talabat.core.Specifications.Product_Spec;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ApiBaseController
{
    private readonly IMapper _mapper;
    private readonly IproductService _productService;


    public ProductsController(IMapper mapper, IproductService productService)
    {
        _mapper = mapper;
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ProductReturnDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pagination<ProductReturnDto>>> GetProducts([FromQuery] ProductSpecparams specParams)
    {
        var products = await _productService.GetProductsAsync(specParams);
        var productsCount = await _productService.GetCountAsync(specParams);

        if (!products.Any())
        {
            return NotFound(new ApiResponse(404, "No products found"));
        }

        var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDto>>(products);
        var pagination = new Pagination<ProductReturnDto>(specParams.PageIndex, specParams.PageSize, productsCount, data);

        return Ok(pagination);
    }

  
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductReturnDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductAsync(id);

        if (product == null)
        {
            return NotFound(new ApiResponse(404, $"Product with ID {id} not found"));
        }

        var productDto = _mapper.Map<ProductReturnDto>(product);
        return Ok(productDto);
    }

  
    [HttpGet("brands")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
    {
        var brands = await _productService.GetBrandsAsync();

        if (!brands.Any())
        {
            return NotFound(new ApiResponse(404, "No brands found"));
        }

        return Ok(brands);
    }

 
    [HttpGet("categories")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductType>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
    {
        var types = await _productService.GetTypesAsync();

        if (!types.Any())
        {
            return NotFound(new ApiResponse(404, "No categories found"));
        }

        return Ok(types);
    }
}

