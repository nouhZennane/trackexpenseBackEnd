using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.User;
using TrackExences.Repositories.UserRepo;
using TrackExences.Services;

namespace TrackExences.Controllers.User;

[Route("api/v1/users")]
[ApiController]
public class CreateController(IUserRepo repo, IValidator<CreateUser> validator, 
    TokenService tokenService) : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateUser cUser)
    {
        var result = validator.Validate(cUser);
        if (result.Errors.Count > 0)
        {
            return BadRequest(new
            {
                message = "Validation Error",
                Errors = result.Errors.Select(x => x.ErrorMessage)
            });
        }

        UserDto userDto = repo.AddUser(cUser);
        string token = tokenService.GenerateToken(
            userDto.Id.ToString(),
            userDto.Email,
            userDto.Name, "user");
        
       // Response.Headers.Append("Authorization", token);
        
        return Ok(new
        {
            message = "User created",
            token,
            user = userDto
        });
    }
}