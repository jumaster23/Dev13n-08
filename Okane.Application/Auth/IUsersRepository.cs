using Okane.Domain;

namespace Okane.Application.Auth;

public interface IUsersRepository : IRepository<User>
{
    User? ByUsername(string username);
}