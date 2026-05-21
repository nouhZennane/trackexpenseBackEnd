using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.Expenses;
using TrackExences.Dtos.User;
using TrackExences.Repositories.ExpenseRepo;
using TrackExences.Repositories.UserRepo;


namespace TrackExences.Controllers.User;



[ApiController]
[Route("api/v1/users")]
public class DisplayController(IUserRepo repo) : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult GetUser([FromRoute] int id)
    {
        UserDto dto = repo.GetUser(id);
        return Ok(new
        {
            message = "Success",
            user = dto
        });
    }
}