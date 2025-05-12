using System.ComponentModel.DataAnnotations;

namespace ContactManagersApi.Models;

public class Person
{
    public int Id { get; set; } //Primary Key
    
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Range(0, 120)]
    public string Phone { get; set; }
}