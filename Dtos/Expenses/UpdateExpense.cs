namespace TrackExences.Dtos.Expenses;

public class UpdateExpense
{

    public double? Amount { get; set; }
    public string? Description { get; set; }
    
    public string? Category { get; set; }
}