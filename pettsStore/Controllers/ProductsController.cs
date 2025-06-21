using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _ProductService;
        public ProductsController(IProductService productsService)
        {
            _ProductService = productsService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery]string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            return await _ProductService.getAllProducts(desc,minPrice, maxPrice,categoryIds);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            return await _ProductService.getProductById(id);
        }
    }
}
