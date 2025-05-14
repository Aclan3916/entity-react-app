using Microsoft.EntityFrameworkCore;
using ContactManagersApi.Data;
using ContactManagersApi.Models;
using ContactManagersApi.interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;

namespace ContactManagersApi.Services;

public enum PatchResult
{
    NotFound,
    InvalidModel,
    Success
}

public class ContactService : IContactService
{
    private readonly PersonDb _context;

    public ContactService(PersonDb context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllPeople()
    {
        return await _context.People.ToListAsync();
    }

    public async Task<Person> GetPersonById(int id)
    {
        return await _context.People.FindAsync(id);
    }

    public async Task<Person> CreatePerson(Person person)
    {
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<bool> DeletePerson(int id)
    {
        var person = await _context.People.FindAsync();
        if (person == null)
        {
            return false;
        }

        _context.People.Remove(person);
        return await _context.SaveChangesAsync() > 0; 
    }

    public async Task<bool> UpdatePersonPut(Person person)
    {
        _context.Entry(person).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PatchResult>UpdatePersonPatch(int id, JsonPatchDocument<Person> patchDocument)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return PatchResult.NotFound;

        patchDocument.ApplyTo(person);

        var context = new ValidationContext(person);
        var results = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(person, context, results, true);
        
        if (!isValid) return PatchResult.InvalidModel;

        await _context.SaveChangesAsync();
        return PatchResult.Success;
    }
}