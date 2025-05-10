namespace ContactManagersApi.Models;

public class Person
{
    public int Id { get; set; } //Primary Key
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
}