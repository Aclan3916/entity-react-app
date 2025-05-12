using Microsoft.AspNetCore.Mvc;
using ContactManagersApi.Data;
using ContactManagersApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using ContactManagersApi.interfaces;
using ContactManagersApi.Services;

namespace ContactManagersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    //private readonly PersonDb _db;

    private readonly IContactService _contactService;

    public PeopleController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IEnumerable<Person>> GetAll()
    {
        return await _contactService.GetAllPeople();
    }

    [HttpPost]
    public async Task<ActionResult<Person>>CreatePerson(Person newPerson)
    {       
            //Accepts a JSON object from the request body
            //Saves it to the database
            //Returns a 201 Created response with the new person’s data
        var createdPerson = await _contactService.CreatePerson(newPerson);

        return CreatedAtAction(nameof(GetAll), new { id = createdPerson.Id }, createdPerson);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var result = await _contactService.DeletePerson(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePersonPut(int id, Person updatedPerson)
    {
        if (id != updatedPerson.Id)
        {
            return BadRequest();
        }

        var result = await _contactService.UpdatePersonPut(updatedPerson);
        if(!result)
            return NotFound();

        return NoContent(); //204 response
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePersonPatch(int id, [FromBody] JsonPatchDocument<Person> patchDoc)
    {
        if (patchDoc == null)
            return BadRequest();

        var result = await _contactService.UpdatePersonPatch(id, patchDoc);

        return result switch
        {
            PatchResult.NotFound => NotFound(),
            PatchResult.InvalidModel => BadRequest("Validation failed."),
            PatchResult.Success => NoContent(),
            _ => StatusCode(500)
        };

        return NoContent();
    }
}

//register a route at /api/people
//injects PersonDb so it can access the data
//implements a GET endpoint to return all people