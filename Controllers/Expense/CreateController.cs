using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Dtos.User;
using TrackExences.Repositories.ExpenseRepo;

namespace TrackExences.Controllers.Expense
{
    [Route("api/v1/expenses")]
    [ApiController]
    public class CreateController(IExpenseRepo repo, IValidator<CreateExpense> validator) : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(CreateExpense cExpense)
        {
            var result = validator.Validate(cExpense);
            if (result.Errors.Count > 0)
            {
                return BadRequest(new
                {
                    message = "Validation Error",
                    Errors = result.Errors.Select(x => x.ErrorMessage)
                });
            }
            var expense = repo.CreateExpense(cExpense);
            return CreatedAtAction("Create", new
            {
                message = "Expense created successfully",
                expense
            });
        }
    }
}
