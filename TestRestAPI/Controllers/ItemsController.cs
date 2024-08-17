using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using TestRestAPI.Data;
using TestRestAPI.Models;
using TestRestAPI.Models.Entities;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace TestRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ItemsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> AllItems()
        {
            var result = await _applicationDbContext.items.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemsById(int id)
        {
            var result = await _applicationDbContext.items.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound($"Item Code {id} is not Exists !");
            }
            return Ok(result);
        }

        [HttpGet("ItemsWithCategorIdItemsWithCategorId/{categoryId}")]
        public async Task<IActionResult> GetItemsWithCategorId(int categoryId)
        {
            var result = await _applicationDbContext.items.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (result == null)
            {
                return NotFound($"Item Code {categoryId} is not Exists !");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] mdlItem mdlitem)
        {
            using var stream = new MemoryStream();
            await mdlitem.Image.CopyToAsync(stream);
            var item = new Item()
            {
                Name = mdlitem.Name,
                Price = mdlitem.Price,
                Notes = mdlitem.Notes,
                CategoryId = mdlitem.CategoryId,
                Image = stream.ToArray()
            };
            await _applicationDbContext.AddAsync(item);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditItem(int id, [FromForm] mdlItem mdl)
        {
            var item = await _applicationDbContext.items.FindAsync(id);
            if (item == null)
            {
                return NotFound($"Item id {id} is not exist");
            }

            var isCategoryExists = await _applicationDbContext.items.AnyAsync(x => x.Id == mdl.CategoryId);
            if (isCategoryExists)
            {
                return NotFound($"Category id {id} is not exist");
            }
            if (mdl.Image == null)
            {
                using var stream = new MemoryStream();
                await mdl.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }
            item.Name = mdl.Name;
            item.Price = mdl.Price;
            item.Notes = mdl.Notes;
            item.CategoryId = mdl.CategoryId;

            await _applicationDbContext.SaveChangesAsync();


            return Ok(item);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteItems(int id)
        {
            var result = await _applicationDbContext.items.SingleOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return NotFound($"The item  id {id} is not exist");
            }
            _applicationDbContext.items.Remove(result);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(result);
        }



    }
}