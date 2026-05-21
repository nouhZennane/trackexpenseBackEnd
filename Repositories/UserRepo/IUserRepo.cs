using TrackExences.Dtos.User;

namespace TrackExences.Repositories.UserRepo;

public interface IUserRepo
{
    public List<UsersDto> GetUsers(int page, int pageSize);
    public UserDto GetUser(int id);
    public UserDto AddUser(CreateUser user);
    public UserDto UpdateUser(UpdateUser user, int id);
    public UserDto DeleteUser(int id);
}