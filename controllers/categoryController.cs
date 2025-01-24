using Microsoft.AspNetCore.Mvc;
using asp_net_ecommerce_web_api.Models;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories")] // Base route for all category-related actions
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>()
        {
            new Category { CategoryId = Guid.NewGuid(), Name = "Category 1", Description = "This is Category 1", CreatedAt = DateTime.Now },
            new Category { CategoryId = Guid.NewGuid(), Name = "Category 2", Description = "This is Category 2", CreatedAt = DateTime.Now },
            // Add more categories as needed for your application.
        };

        // GET: /api/categories
        [HttpGet]
        public IActionResult GetCategories([FromQuery] string? searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                var searchedCategories = categories
                    .Where(category => category?.Name?.Contains(searchValue, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();

                return Ok(searchedCategories);
            }

            return Ok(categories);
        }

        // POST: /api/categories
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Name is required");
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.Now
            };

            categories.Add(newCategory);

            return CreatedAtAction(nameof(GetCategories), new { id = newCategory.CategoryId }, newCategory);
        }

        // DELETE: /api/categories/{categoryId}
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return NotFound(new { Message = "Category not found." });
            }

            categories.Remove(foundCategory);
            return NoContent();
        }

        // PUT: /api/categories
      [HttpPut("{categoryId:guid}")]
public IActionResult UpdateCategory(Guid categoryId, [FromBody] Category updatedCategory)
{
    // Find the category with the given ID
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);

    if (foundCategory == null)
    {
        // Return 404 Not Found if the category doesn't exist
        return NotFound(new { Message = "Category to update not found." });
    }

    // Update the category's properties
    if (!string.IsNullOrEmpty(updatedCategory.Name))
    {
        foundCategory.Name = updatedCategory.Name;
    }

    if (!string.IsNullOrEmpty(updatedCategory.Description))
    {
        foundCategory.Description = updatedCategory.Description;
    }

    foundCategory.CreatedAt = DateTime.Now; // Optionally update the timestamp

    // Return the updated category
    return Ok(foundCategory);
}

    }
}
