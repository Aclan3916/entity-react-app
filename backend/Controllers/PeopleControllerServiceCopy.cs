// using Microsoft.AspNetCore.Mvc;
// using ContactManagersApi.Models;
// using Microsoft.AspNetCore.JsonPatch;
// using ContactManagersApi.interfaces;
// using ContactManagersApi.Services;
// //For practice 
// namespace ContactManagersApi.Controllers;
// --was made for practice
// [ApiController]
// [Route("/api/[controller]")]
// public class PeopleControllerServiceCopy : ControllerBase
// {
//     private readonly IContactService _contactService;
//
//     public PeopleControllerServiceCopy(IContactService contactService)
//     {
//         _contactService = contactService;
//     }
//
//     [HttpGet]
//     public async Task<IEnumerable<Person>> GetAllPeople()
//     {
//         return await _contactService.GetAllPeople();
//     }
//
//     [HttpPost]
//     public async Task<ActionResult> AddPerson(Person person)
//     {
//         var newPerson = await _contactService.CreatePerson(person);
//         
//         return CreatedAtAction(nameof(GetAllPeople), new {id = newPerson.Id}, newPerson)
//     }
//
//     [HttpDelete]
//     public async Task<IActionResult> DeletePerson(int id)
//     {
//         var person = await _contactService.DeletePerson(id);
//
//         if (!person)
//         {
//             return NotFound();
//         }
//
//         return NoContent();
//     }
// } 