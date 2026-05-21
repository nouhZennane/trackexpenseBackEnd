namespace TrackExences.Dtos.Expenses;

public class CreateExpense
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    public double Amount { get; set; }
    
    public string Description { get; set; }
    
    public string Category { get; set; }
    
    public int UserId { get; set; }
}