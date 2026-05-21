using TrackExences.Dtos.Expenses;
using TrackExences.Models;

namespace TrackExences.Services;

public class ExpenseMapperService(UserMapperService userMapperService)
{
    public Expense CreateExpenseToExpense(CreateExpense cExpenses)
    {
        Expense expense = new Expense
        {
            Amount = cExpenses.Amount,
            Category = cExpenses.Category,
            Date = cExpenses.Date,
            Description = cExpenses.Description,
            UserId = cExpenses.UserId,
        };
        return expense;
    }

    public ExpenseDto ExpenseToExpenseDto(in Expense expense)
    {
        ExpenseDto expenseDto = new()
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Category = expense.Category,
            Date = expense.Date,
            Description = expense.Description,
            Creator = expense.User == null ? null : userMapperService.CreateUserDto(expense.User),
        };
        return expenseDto;
    }
    
    public void UpdateExpense(UpdateExpense updateExpense, in Expense expense)
    {
       expense.Amount =  updateExpense.Amount ??  expense.Amount;
       expense.Category = updateExpense.Category ??  expense.Category;
       expense.Description = updateExpense.Description ??  expense.Description;
    }
    
}