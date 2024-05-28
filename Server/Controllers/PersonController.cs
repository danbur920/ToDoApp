using BlazorApp1.Server.Entities;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly TodoDbContext _context;
        public PersonController(TodoDbContext context)
        {
            _context = context;
        }

        // GET:

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{

        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        // Wszyscy pracownicy
        [HttpGet("/person/all")]
        public IActionResult GetPerson()
        {
            var people = _context.Persons.ToArray();
            return Ok(people);
        }

        // Wszyscy pracownicy z zadaniami
        [HttpGet("/person/allwithtasks")]
        public IActionResult GetAllPersonsWithTasks()
        {
            var peopleWithTasks = _context.Persons.Include(p => p.Tasks).ToList();
            return Ok(peopleWithTasks);
        }

        // Wybrany pracownik z podanym id
        [HttpGet("/person/one")]
        public IActionResult GetPersonById(int id)
        {
            var people = _context.Persons.Where(x => x.Id == id);
            return Ok(people);
        }

        // DELETE:

        // Usun pracownika o podanym id
        [HttpDelete("/person/delete/{id}")]
        public IActionResult DeletePerson(int id)
        {
            var personToDelete = _context.Persons.Find(id);

            if (personToDelete is null)
                return NotFound();

            _context.Persons.Remove(personToDelete);
            _context.SaveChanges();

            return NoContent();
        }

        // POST:

        // Dodaj pracownika
        [HttpPost("/person/add")]
        public IActionResult AddPerson([FromBody] Person newPerson)
        {
            if (newPerson == null)
                return BadRequest("Invalid data. Person cannot be null.");

            try
            {
                _context.Persons.Add(newPerson);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetPersonById), new { id = newPerson.Id }, newPerson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        // PATCH:

        // Aktualizuj dane pracownika
        [HttpPatch("/person/update")]
        public IActionResult UpdatePerson([FromBody] Person updatedPerson)
        {
            if (updatedPerson == null)
                return BadRequest("Invalid data. Person cannot be null.");

            var existingPerson = _context.Persons.FirstOrDefault(x => x.Id == updatedPerson.Id);

            if (existingPerson == null)
                return NotFound("Person not found.");

            // Sprawdzamy za pomocą ?? czy wartość nie jest nullem.
            existingPerson.FirstName = updatedPerson.FirstName ?? existingPerson.FirstName;
            existingPerson.LastName = updatedPerson.LastName ?? existingPerson.LastName;
            existingPerson.Email = updatedPerson.Email ?? existingPerson.Email;

            _context.SaveChanges();
            return Ok(existingPerson);
        }
    }
}
