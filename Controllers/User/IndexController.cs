using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Dtos.User;
using TrackExences.Repositories.ExpenseRepo;
using TrackExences.Repositories.UserRepo;

namespace TrackExences.Controllers.User;

[Route("api/v1/users")]
[ApiController]
public class IndexController(IUserRepo repo) : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 4)
    {
        List<UsersDto> users = repo.GetUsers(page, pageSize);
        return Ok(new
        {
            page,
            pageSize,
            users,
        });
    }
}