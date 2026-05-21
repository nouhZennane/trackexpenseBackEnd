using Microsoft.AspNetCore.Mvc;
using TrackExences.Dtos.User;
using TrackExences.Repositories.UserRepo;

namespace TrackExences.Controllers.User;

public class DeleteController(IUserRepo repo): ControllerBase
{
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute]int id)
    {
        UserDto deleted= repo.DeleteUser(id);
        return Ok(
            new
            {
                message = "Expense deleted successfully",
                expense = deleted
            });
    }
}