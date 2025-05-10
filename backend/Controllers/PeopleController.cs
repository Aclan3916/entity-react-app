using Microsoft.AspNetCore.Mvc;
using ContactManagersApi.Data;
using ContactManagersApi.Models;

namespace ContactManagersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly PersonDb _db;

    public PeopleController(PersonDb db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        return Ok(_db.People.ToList());
    }

    [HttpPost]
    public ActionResult<Person> CreatePerson([FromBody] Person newPerson)
    {       
            //Accepts a JSON object from the request body
            //Saves it to the database
            //Returns a 201 Created response with the new person’s data
        _db.People.Add(newPerson);
        _db.SaveChanges();

        return CreatedAtAction(nameof(GetAll), new { id = newPerson.Id }, newPerson);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
    {
        var person = _db.People.FirstOrDefault(p => p.Id == id);
        if(person == null)
            return NotFound();

        person.Name = updatedPerson.Name;
        person.Email = updatedPerson.Email;
        person.Phone = updatedPerson.Phone;

        _db.SaveChanges();

        return NoContent(); //204 response
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePerson(int id)
    {
        var person = _db.People.FirstOrDefault(x => x.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        _db.People.Remove(person);
        _db.SaveChanges();

        return NoContent();
    }
    
}

//register a route at /api/people
//injects PersonDb so it can access the data
//implements a GET endpoint to return all people