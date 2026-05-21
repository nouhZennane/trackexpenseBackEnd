using System.Text.Json.Serialization;
using TrackExences.Dtos.User;

namespace TrackExences.Dtos.Expenses;

public class ExpenseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserDto? Creator { get; set; }
}