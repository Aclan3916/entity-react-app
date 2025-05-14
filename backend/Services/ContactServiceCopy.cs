// namespace ContactManagersApi.Services;
// using Microsoft.EntityFrameworkCore;
// using ContactManagersApi.Data;
// using ContactManagersApi.Models;
// using ContactManagersApi.interfaces;
// using Microsoft.AspNetCore.JsonPatch;
// using System.ComponentModel.DataAnnotations;
// -- was made for practice
// public class ContactServiceCopy : IContactService
// {
//     private readonly PersonDb _context;
//
//     public ContactServiceCopy(PersonDb context)
//     {
//         _context = context;
//     }
//
//     public async Task<IEnumerable<Person>> GetAllPeople()
//     {
//         return await _context.People.ToListAsync();
//     }
//
//     public async Task<Person> AddPerson(Person newPerson)
//     {
//         _context.Add(newPerson);
//         await _context.SaveChangesAsync();
//         return newPerson;
//     }
//
//     public async Task<bool> DeletePerson(int id)
//     {
//         var person = await _context.People.FindAsync();
//         if (person == null)
//         {
//             return false;
//         }
//
//         _context.People.Remove(person);
//         return await _context.SaveChangesAsync() > 0;
//     }
// }