using webapi.Database.Interface;
using webapi.Domain.Entity.Auth;

namespace webapi.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly Db _db;
    public UserRepository(Db db)
    {
        _db = db;
    }

    public bool IsExist(string username)
    {
        return _db.Users.Any(u => u.Username == username);
    }
    public User? FindByUsername(string username) {
        var userModel = _db.Users.FirstOrDefault(u => u.Username == username);
        if (userModel == null) return null;
        return new User
        {
            Id = userModel.Id,
            Username = userModel.Username,
            PasswordHash = userModel.PasswordHash,
            Role = userModel.Role,
        };
    }

    public long CreateUser(User user)
    {
        var userModel = new Model.User
        {
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            Role = user.Role
        };

        _db.Users.Add(userModel);
        _db.SaveChanges();
        return userModel.Id;
    } 
}