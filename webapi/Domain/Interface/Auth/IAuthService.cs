using webapi.Domain.Entity.Auth;

namespace webapi.Domain.Interface.Auth;


public interface IAuthService
{
    public bool Registry(RegistrationCredentials registrationCredentials);
    public User? AuthenticateUser(Credentials credentials);
}