using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Middlewares;
using TrackExences.Repositories.ExpenseRepo;

namespace TrackExences.Controllers.Expense
{
    [Route("api/v1/expenses")]
    [ApiController]
    [Authorize]
   // [JwtFilter]
    public class IndexController(IExpenseRepo repo) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery]int page = 1, [FromQuery]int pageSize = 4)
        {
            int userId = 1;
            List<ExpenseDto> expenses =  repo.GetExpenses(page, pageSize, userId);
            return Ok(new
            {
                page,
                pageSize,
                expenses,
            });
        }
    }
}
