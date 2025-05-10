using Microsoft.EntityFrameworkCore;
using ContactManagersApi.Models;

namespace ContactManagersApi.Data;

//AppDbContext gateway to the database.
//manages models (tables) and handles communication with the database
public class PersonDb : DbContext
{
    //represents a table of Contact records 
    //EF core will use this to create a contacts table and let you
    //query/add/update/delete rows
    public DbSet<Person> People { get; set; }

    //constructor to accept DbContextOptions- this is how ASP.NET Core inject config
    public PersonDb(DbContextOptions<PersonDb> options) : base(options)
    {
        
    }
    
}   