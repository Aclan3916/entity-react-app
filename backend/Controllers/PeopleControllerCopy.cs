using Microsoft.AspNetCore.Mvc;
using ContactManagersApi.Data;
using ContactManagersApi.Models;
using Microsoft.AspNetCore.JsonPatch;


namespace ContactManagersApi.Controllers;
//this is how we did it when we did not have a service
[ApiController]
[Route("/api/[controller]")]
public class PeopleControllerCopy : ControllerBase
{
    private readonly PersonDb _db;

    //build a constructor where we inject our dependency 
    public PeopleControllerCopy(PersonDb db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
       return Ok(_db.People.ToList());
    }

    [HttpPost]
    public ActionResult<Person> AddPerson([FromBody] Person newPerson)
    {
        try
        {
            _db.People.Add(newPerson);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetAll), new { id = newPerson.Id }, newPerson);

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex:Message}");
        }
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

    [HttpPut("{id}")]
    public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
    {
        var person = _db.People.FirstOrDefault(x => x.Id == id);
        
        if(person == null) return NotFound();

        person.Name = updatedPerson.Name;
        person.Phone = updatedPerson.Phone;
        person.Email = updatedPerson.Email;
        
        _db.SaveChanges();
        
        return NoContent(); //204 response
    }
    
    [HttpPatch("{id}")]
    public IActionResult PatchPerson(int id, [FromBody] JsonPatchDocument<Person> patchDoc)
    {
        if (patchDoc == null)
            return BadRequest();

        var person = _db.People.FirstOrDefault(x => x.Id == id);
        if (person == null)
            return NotFound();

        patchDoc.ApplyTo(person, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _db.SaveChanges();

        return NoContent(); // or Ok(person) if you want to return the updated object
    }
    
   
}