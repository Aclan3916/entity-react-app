using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManagersApi.Models;
using ContactManagersApi.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace ContactManagersApi.interfaces;

public interface IContactService
{
    Task<IEnumerable<Person>> GetAllPeople();
    Task<Person> GetPersonById(int id);
    Task<Person> CreatePerson(Person person);
    Task<bool> UpdatePersonPut(Person updatedPerson);
    Task<PatchResult> UpdatePersonPatch(int id, JsonPatchDocument<Person> patchDoc);
    Task<bool> DeletePerson(int id);
}