using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRestAPI.Data;
using TestRestAPI.Models.Entities;

namespace TestRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoriesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _applicationDbContext.categories.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryByID(int id)
        {
            var result= await _applicationDbContext.categories.SingleOrDefaultAsync(c => c.Id == id);
            if(result == null)
            {
                return NotFound($"Category id {id} is not exist");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var category1 = new Category()
            {
                Name = category.Name,
            };
            var result = await _applicationDbContext.categories.AddAsync(category1);
            _applicationDbContext.SaveChanges();
            return Ok(result);
        }

       

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var result = await _applicationDbContext.categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (result == null)
            {
                return BadRequest("$The ID {category.Id} is not exist ");
            }
            result.Name = category.Name;
            _applicationDbContext.SaveChanges();
            return Ok(result);
        }

        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleatCategory(int id)
        {
            var result = await _applicationDbContext.categories.SingleOrDefaultAsync(x=> x.Id == id);
            if (result == null)
            {
                return BadRequest($"Categor {id}is not id found");
            }
            _applicationDbContext.Remove(result);
            _applicationDbContext.SaveChanges();
            return Ok(result);
        }



    }
}
