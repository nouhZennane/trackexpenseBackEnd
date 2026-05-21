using TrackExences.Dtos.Expenses;
using TrackExences.Models;

namespace TrackExences.Repositories.ExpenseRepo;

public interface IExpenseRepo
{
    public List<ExpenseDto> GetExpenses(int page, int pageSize, int userId);
    public ExpenseDto GetExpense(int id);
    
    public ExpenseDto CreateExpense(CreateExpense expense);
    
    public ExpenseDto UpdateExpense(UpdateExpense expense, int id);
    public ExpenseDto DeleteExpense(int id);
}