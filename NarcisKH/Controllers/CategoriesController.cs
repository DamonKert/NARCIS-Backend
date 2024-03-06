using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarcisKH.Class;
using NarcisKH.Data;
using NarcisKH.Models;

namespace NarcisKH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NarcisKHContext _context;

        public CategoriesController(NarcisKHContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories =  await _context.Categories.Include(x=>x.Children).ToListAsync();
            if(categories.Count == 0)
            {
                var notFoundresponse = new
                {
                    StatusCode = 404,
                    Message = "No Categories Found"
                };
                return NotFound(notFoundresponse);
            }
            var categoriesResult = new List<GetCategoriesResponse>();
            foreach(var category in categories)
            {
                GetCategoriesResponse categoriesResponse = new GetCategoriesResponse()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Children = category.Children,
                    Parent = category.Parent,
                };
                categoriesResult.Add(categoriesResponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Categories Found",
                Data = categoriesResult
            };
            return Ok(successResponse);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                var notFoundresponse = new
                {
                    StatusCode = 404,
                    Message = "Category Not Found"
                };
                return NotFound(notFoundresponse);
            }
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Category Found",
                Data = category
            };
            return Ok(successResponse);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("EditCategory/{id}")]
        public async Task<IActionResult> EditCategory(UpdateCategoryRequest updateCategory)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == updateCategory.ID);
            if(category == null)
            {
                return NotFound(new { StatusCode = 404, Message = "Category Not Found" });
            }
            category.Name = updateCategory.Name;
            if(updateCategory.ParentId != 0)
            {
                var parentCategory = _context.Categories.FirstOrDefault(x => x.Id == updateCategory.ParentId);
                if(parentCategory == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "Parent Category Not Found" });
                }
                category.ParentId = updateCategory.ParentId;
                category.Parent = parentCategory;
            }
            else
            {
                category.ParentId = null;
            }
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Category Updated",
                Data = category
            };
            return Ok(successResponse);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryRequest category)
        {
            Category newCategory = new Category()
            {
                Name = category.Name,
            };

           if(category.ParentId != 0)
            {
                var parentCategory = _context.Categories.FirstOrDefault(x => x.Id == category.ParentId);
                if(parentCategory == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "Parent Category Not Found" });
                }
                newCategory.ParentId = category.ParentId;
                newCategory.Parent = parentCategory;
            }
           _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

           var successResponse = new
           {
               StatusCode = 200,
               Message = "Category Created",
               Data = newCategory
           };
            return Ok(successResponse);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound(new { StatusCode = 404, Message = "Category Not Found" });
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            var successResponse = new
            {
                StatusCode = 200,
                Message = "Category Deleted",
            };
            return Ok(successResponse);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
