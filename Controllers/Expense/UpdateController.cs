using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Repositories.ExpenseRepo;

namespace TrackExences.Controllers.Expense;

[Route("api/v1/expenses")]
[ApiController]
public class UpdateController(IExpenseRepo repo, IValidator<UpdateExpense> validator) : ControllerBase
{
    [HttpPatch("{id:int}")]
    public IActionResult UpdateExpense(UpdateExpense uExp, [FromRoute] int id)
    {
        var result = validator.Validate(uExp);
        if (result.Errors.Count > 0)
        {
            return BadRequest(new
            {
                message = "Invalid data",
                errors = result.Errors.Select(x => x.ErrorMessage)
            });
        }

        ExpenseDto dto = repo.UpdateExpense(uExp, id);
        return Ok(new
        {
            message = "Expense updated successfully",
            expense = dto
        });
    }
}