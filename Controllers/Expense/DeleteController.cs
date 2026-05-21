using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Repositories.ExpenseRepo;

namespace TrackExences.Controllers.Expense
{
    [Route("api/v1/expenses")]
    [ApiController]
    public class DeleteController(IExpenseRepo repo) : ControllerBase
    {
        [HttpDelete("{id:int}")]
        public IActionResult DeleteExpense([FromRoute]int id)
        {
           ExpenseDto deleted= repo.DeleteExpense(id);
            return Ok(
                new
                {
                    message = "Expense deleted successfully",
                    expense = deleted
                });
        }
    }
}
