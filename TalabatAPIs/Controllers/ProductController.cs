using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;
using Talabat.Repository;

namespace TalabatAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryrepo;
        private readonly IMapper _mapper;

        public ProductController(
                                   IGenericRepository<Product> productsRepo,
                                   IGenericRepository<ProductBrand> brandRepo,
                                   IGenericRepository<ProductCategory>categoryrepo,
                                   IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandRepo = brandRepo;
            _categoryrepo = categoryrepo;
            _mapper = mapper;
        }
        //  /api/Products
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var Spec = new ProductWithBrandAndCategorySpecifications(specParams);

            var products = await _productsRepo.GetAllWithSpecAsync(Spec);

            var data = _mapper.Map< IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products) ;

            var countSpec = new ProductsWithFilterationForCountSpecification(specParams);
            var count = await _productsRepo.GetCountAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize,count,data));
        }


        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var Spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await _productsRepo.GetWithSpecAsync(Spec);
            if (product is null)
                return NotFound(new ApiResponse(404)); //404
            return Ok(_mapper.Map<Product , ProductToReturnDto>(product)); //200
        }

        [HttpGet("brands")] // Get : api/products/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")] // Get : api/products/categories
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetCategories()
        {
            var categories = await _brandRepo.GetAllAsync();
            return Ok(categories);
        }


    }
}
