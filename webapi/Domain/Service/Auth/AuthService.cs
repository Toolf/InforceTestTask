using System.Security.Cryptography;
using System.Text;
using webapi.Database.Interface;
using webapi.Database.Repository;
using webapi.Domain.Entity.Auth;
using webapi.Domain.Interface.Auth;

namespace webapi.Domain.Service.Auth;


public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public bool Registry(RegistrationCredentials registrationCredentials)
    {
        if (_userRepository.IsExist(registrationCredentials.Username))
        {
            return false; // User already exists
        }

        var user = new Entity.Auth.User
        {
            Username = registrationCredentials.Username,
            PasswordHash = HashPassword(registrationCredentials.Password), // Hash and store the password
            Role = registrationCredentials.Role
        };

        _userRepository.CreateUser(user);

        return true;
    }

    public Entity.Auth.User? AuthenticateUser(Credentials credentials)
    {
        var user = _userRepository.FindByUsername(credentials.Username);

        if (user == null)
            return null; // User not found

        if (!VerifyPassword(credentials.Password, user.PasswordHash))
            return null; // Password does not match

        return user;
    }

    private static string HashPassword(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }

    private static bool VerifyPassword(string inputPassword, string storedHash)
    {
        var hashedInput = HashPassword(inputPassword);
        return string.Equals(hashedInput, storedHash, StringComparison.OrdinalIgnoreCase);
    }
}