using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.User;
using TrackExences.Repositories.UserRepo;

namespace TrackExences.Controllers.User;

public class UpdateController(IUserRepo repo, IValidator<UpdateUser> validator): ControllerBase
{
    [HttpPatch("{id:int}")]
    public IActionResult UpdateExpense(UpdateUser uExp, [FromRoute] int id)
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

        UserDto dto = repo.UpdateUser(uExp, id);
        return Ok(new
        {
            message = "Expense updated successfully",
            expense = dto
        });
    }
}