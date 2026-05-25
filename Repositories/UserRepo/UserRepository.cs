using Microsoft.EntityFrameworkCore;
using TrackExences.Data;
using TrackExences.Dtos.User;
using TrackExences.Models;
using TrackExences.Services;

namespace TrackExences.Repositories.UserRepo;

public class UserRepository(
    UserMapperService mapper,
    AppDbContext context
) : IUserRepo
{
    public List<UsersDto> GetUsers(int page, int pageSize)
    {
        // for admin to see all users 
        List<UsersDto> users = context.Users
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(user => mapper.CreateUsersDto(user))
            .ToList();
        return users;
    }

    public UserDto GetUser(int id)
    {
        User? user = CheckIfUserExists(id);
        UserDto dto = mapper.CreateUserDto(user);
        return dto;
    }

    public UserDto AddUser(CreateUser cUser)
    {
        User user = mapper.CreateUserToUser(cUser);
        var savedUser = context.Users.Add(user);
        context.SaveChanges();
     
        return mapper.CreateUserDto(user);
    }


    public UserDto UpdateUser(UpdateUser uUser, int id)
    {
        User user = CheckIfUserExists(id);
        mapper.UpdateUserToUser(uUser, user);
        UserDto userDto = mapper.CreateUserDto(user);
        context.SaveChanges();
        return userDto;
    }

    public UserDto DeleteUser(int id)
    {
        User user = CheckIfUserExists(id);
        context.Users.Remove(user);
        context.SaveChanges();
        return mapper.CreateUserDto(user);
    }

    private User CheckIfUserExists(int id)
    {
        User? user = context.Users
            .Include(u => u.Expenses)
            .FirstOrDefault(e => e.Id == id);
        if (user == null)
        {
            throw new BadHttpRequestException($"User not found: {id}");
        }

        return user;
    }
}