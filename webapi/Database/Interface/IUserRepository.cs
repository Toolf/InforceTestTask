using webapi.Domain.Entity.Auth;

namespace webapi.Database.Interface;

public interface IUserRepository
{

    public bool IsExist(string username);
    public User? FindByUsername(string username);

    public long CreateUser(User user);
}