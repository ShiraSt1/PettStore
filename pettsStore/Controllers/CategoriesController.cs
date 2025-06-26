using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
//delete unsuded code
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _CategoryService;// _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _CategoryService = categoryService;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            return await _CategoryService.getAllCategories();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            return await _CategoryService.getCategoryById(id);
        }
    }
}
