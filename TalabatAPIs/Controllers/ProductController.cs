using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace TalabatAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        public ProductController(IGenericRepository<Product> productsRepo)
        {
            _productsRepo = productsRepo;
        }
        //  /api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new BaseSpecifications<Product>();
            var products = await _productsRepo.GetAllWithSpecAsync(Spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsRepo.GetAsync(id);
            if (product is null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 }); //404
            return Ok(product); //200
        }

    }
}
