namespace TrackExences.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string nickname { get; set; }
    public DateTime Created { get; init; } =  DateTime.UtcNow;

    public List<Expense> Expenses { get; set; } = [];
}