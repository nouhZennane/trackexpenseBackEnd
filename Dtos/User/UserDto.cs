using System.Text.Json.Serialization;
using TrackExences.Dtos.Expenses;
using TrackExences.Models;

namespace TrackExences.Dtos.User;

public class UsersDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }

    public string nickname { get; set; }
    
    
   

    
}

public class UserDto: UsersDto
{

    public List<ExpenseDto>? Expenses { get; set; } 
}
