using BlazorApp1.Server.Entities;
using Microsoft.AspNetCore.Mvc;
using BlazorApp1.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorApp1.Server.Controllers
{
    public class TaskController : Controller
    {
        private readonly TodoDbContext _context;
        public TaskController(TodoDbContext context)
        {
            _context = context;
        }

        // GET:

        // Wszystkie zadania
        [HttpGet("/task/all")]
        public IActionResult GetTask()
        {
            var tasks = _context.Tasks.ToArray();
            return Ok(tasks);
        }

        // Wszystkie zadania z pracownikami
        [HttpGet("/task/allWithPeople")]
        public IActionResult GetAllTasksWithPeople()
        {
            var tasksWithPeople = _context.Tasks.Include(p => p.Person).ToArray();
            return Ok(tasksWithPeople);
        }

        // Wszystkie zadania z kategoriami
        [HttpGet("/task/allWithCategories")]
        public IActionResult GetAllTasksWithCategories()
        {
            var tasksWithCategories = _context.Tasks.Include(t => t.Category).ToArray();
            return Ok(tasksWithCategories);
        }


        // Wybrane zadanie z podanym id
        [HttpGet("/task/one")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Where(x => x.Id == id);
            return Ok(task);
        }

        // DELETE:

        // Usun zadanie o podanym id
        [HttpDelete("/task/delete/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var taskToDelete = _context.Tasks.Find(id);
            if (taskToDelete == null)
                return NotFound();

            _context.Tasks.Remove(taskToDelete);
            _context.SaveChanges();

            return NoContent();
        }

        // POST:

        // Dodaj zadanie
        [HttpPost("/task/add")]
        public IActionResult AddTask([FromBody] Entities.Task newTask)
        {
            if (newTask == null)
                return BadRequest("Invalid data. Task cannot be null.");

            try
            {
                _context.Tasks.Add(newTask);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        // PATCH:

        // Aktualizuj dane zadania
        [HttpPatch("/task/update")]
        public IActionResult UpdateTask([FromBody] Entities.Task updatedTask)
        {
            if (updatedTask == null)
                return BadRequest("Invalid data. Task cannot be null.");

            var existingTask = _context.Tasks.FirstOrDefault(x => x.Id == updatedTask.Id);

            if (existingTask == null)
                return NotFound("Task not found.");

            // Sprawdzamy za pomocą ?? czy wartość nie jest nullem.
            existingTask.Title = updatedTask.Title ?? existingTask.Title;
            existingTask.Description = updatedTask.Description ?? existingTask.Description;

            if (existingTask.Deadline != updatedTask.Deadline)
                existingTask.Deadline = updatedTask.Deadline;

            if (existingTask.IsCompleted != updatedTask.IsCompleted)
                existingTask.IsCompleted = updatedTask.IsCompleted;

            if (existingTask.CategoryId != updatedTask.CategoryId)
                existingTask.CategoryId = updatedTask.CategoryId;

            if (existingTask.PersonId != updatedTask.PersonId)
                existingTask.PersonId = updatedTask.PersonId;

            _context.SaveChanges();
            return Ok(existingTask);
        }
    }
}

