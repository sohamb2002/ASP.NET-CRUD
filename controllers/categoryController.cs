using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_net_ecommerce_web_api.Models;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/categories
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] string? searchValue)
        {
            var categories = await _context.Categories
                .Where(category => string.IsNullOrEmpty(searchValue) ||
                                   EF.Functions.ILike(category.Name ?? "", $"%{searchValue}%"))
                .ToListAsync();

            return Ok(new ApiResponse<List<Category>>(categories, 200, "Categories fetched successfully."));
        }

        // POST: /api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest(new ApiResponse<string>(null, 400, "Name is required."));
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.Now
            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories),
                new { id = newCategory.CategoryId },
                new ApiResponse<Category>(newCategory, 201, "Category created successfully."));
        }

        // DELETE: /api/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var foundCategory = await _context.Categories.FindAsync(categoryId);

            if (foundCategory == null)
            {
                return NotFound(new ApiResponse<string>(null, 404, "Category not found."));
            }

            _context.Categories.Remove(foundCategory);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<string>("Category deleted successfully.", 200));
        }

        // PUT: /api/categories/{categoryId}
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto updatedCategory)
        {
            var foundCategory = await _context.Categories.FindAsync(categoryId);

            if (foundCategory == null)
            {
                return NotFound(new ApiResponse<string>(null, 404, "Category to update not found."));
            }

            if (!string.IsNullOrEmpty(updatedCategory.Name))
            {
                foundCategory.Name = updatedCategory.Name;
            }

            if (!string.IsNullOrEmpty(updatedCategory.Description))
            {
                foundCategory.Description = updatedCategory.Description;
            }

            foundCategory.CreatedAt = DateTime.Now;

            _context.Categories.Update(foundCategory);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<Category>(foundCategory, 200, "Category updated successfully."));
        }
    }
}
