using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TrackExences.Data;
using TrackExences.Dtos.Expenses;
using TrackExences.Models;
using TrackExences.Services;

namespace TrackExences.Repositories.ExpenseRepo;

public class ExpensesRepository(ExpenseMapperService mapper, AppDbContext context) : IExpenseRepo
{
    public List<ExpenseDto> GetExpenses(int page, int pageSize, int userId)
    {
        var query = context.Expenses.AsQueryable();
        
        int totalCount = query.Count();
        
        List<ExpenseDto> expenses = query
                .Where(e => e.UserId == 1)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(ex => mapper.ExpenseToExpenseDto(ex))
                .ToList()
            ;
        return expenses;
    }

    public ExpenseDto GetExpense(int expenseId)
    {
        Expense expense = CheckIfExpenseExists(expenseId);
        var dto = mapper.ExpenseToExpenseDto(expense);
        return dto;
    }

    public ExpenseDto CreateExpense(CreateExpense cExpense)
    {
        Expense expense = mapper.CreateExpenseToExpense(cExpense);
        context.Expenses.Add(expense);
        context.SaveChanges();
        ExpenseDto dto = mapper.ExpenseToExpenseDto(expense);

        return dto;
    }

    public ExpenseDto UpdateExpense(UpdateExpense uExp, int id)
    {
        Expense expense = CheckIfExpenseExists(id);
        mapper.UpdateExpense(uExp, expense);
        context.SaveChanges();
        return mapper.ExpenseToExpenseDto(expense);
    }

    public ExpenseDto DeleteExpense(int id)
    {
        Expense expense = CheckIfExpenseExists(id);
        context.Expenses.Remove(expense);
        context.SaveChanges();
        return mapper.ExpenseToExpenseDto(expense);
    }

    private Expense CheckIfExpenseExists(int id)
    {
        Expense? expense = context.Expenses
            .Include(u => u.User)
            .FirstOrDefault(e => e.Id == id);
        if (expense == null)
        {
            throw new BadHttpRequestException($"Expense not found: {id}");
        }

        return expense;
    }
}