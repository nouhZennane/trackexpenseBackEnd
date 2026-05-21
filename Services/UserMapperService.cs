using TrackExences.Dtos.Expenses;
using TrackExences.Dtos.User;
using TrackExences.Models;

namespace TrackExences.Services;

public class UserMapperService(HasherService hasherService)
{
    public User CreateUserToUser(CreateUser cUser)
    {
        User user = new User
        {
            Name = cUser.Name,
            Email = cUser.Email,
            Password = hasherService.HashPassword(cUser.Password),
            nickname = cUser.nickname,
        };
        return user;
    }

    public void UpdateUserToUser(UpdateUser uUser, in User user)
    {
        user.Name = uUser.Name ?? user.Name;
        user.Email = uUser.Email ?? user.Email;
        user.Password = uUser.Password == null ? user.Password : hasherService.HashPassword(uUser.Password);
        user.nickname = uUser.nickname ?? user.nickname;
    }

    public UserDto CreateUserDto(User user)
    {
        UserDto uDto = new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            nickname = user.nickname,
            Expenses = user.Expenses.Select(ex => new ExpenseDto
                {
                    Id = ex.Id,
                    Amount = ex.Amount,
                    Description = ex.Description,
                    Date = ex.Date,
                    Category =  ex.Category,
                    
                } )
                .ToList()
        };
        return uDto;
    }
    public UsersDto CreateUsersDto(User user)
    {
        UsersDto uDto = new UsersDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            nickname = user.nickname,
        };
        return uDto;
    }
}