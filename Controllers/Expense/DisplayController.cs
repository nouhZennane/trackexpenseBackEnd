using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Repositories.ExpenseRepo;

namespace TrackExences.Controllers.Expense;

[ApiController]
[Route("api/v1/expenses")]
public class DisplayController(IExpenseRepo repo) : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult GetExpense([FromRoute] int id = 0)
    {
        ExpenseDto dto = repo.GetExpense(id);
        return Ok(new
        {
            message = "Success",
            expense = dto
        });
    }
}