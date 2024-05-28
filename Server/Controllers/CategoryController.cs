using BlazorApp1.Server.Entities;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly TodoDbContext _context;
        public CategoryController(TodoDbContext context)
        {
            _context = context;
        }

        // GET:

        // Wszystkie kategorie
        [HttpGet("/categories/all")]
        public IActionResult GetCategory()
        {
            var categories = _context.Categories.ToArray();
            return Ok(categories);
        }

        // Wybrana kategoria
        [HttpGet("/categories/one")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return Ok(category);
        }

        // POST:

        // Dodaj kategorię
        [HttpPost("/categories/add")]
        public IActionResult AddCategory([FromBody] Category newCategory)
        {
            if (newCategory == null)
                return BadRequest("Invalid data. Category cannot be null.");

            try
            {
                _context.Categories.Add(newCategory);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetCategoryById), new { id = newCategory.Id }, newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        // DELETE:

        // Usuwanie kategorii o podanym id
        [HttpDelete("/categories/delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var categoryToDelete = _context.Categories.Find(id);

            if (categoryToDelete == null)
                NotFound();

            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            return NoContent();
        }

        // PATCH:

        // Zaktualizuj kategorię:
        [HttpPatch("/categories/update")]
        public IActionResult UpdateCategory([FromBody] Category updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest("Invalid data. Category cannot be null.");

            var existingCategory = _context.Categories.FirstOrDefault(x => x.Id == updatedCategory.Id);

            if (existingCategory == null)
                return NotFound("Category not found.");

            // Sprawdzamy za pomocą ?? czy wartość nie jest nullem.
            existingCategory.Name = updatedCategory.Name ?? existingCategory.Name;
            existingCategory.Description = updatedCategory.Description ?? existingCategory.Description;

            _context.SaveChanges();
            return Ok(existingCategory);
        }
    }
}
