using Okane.Application.Auth;
using Okane.Domain;

namespace Okane.Storage.InMemory;

public class InMemoryUsersRepository : InMemoryRepository<User>, IUsersRepository
{
    public User? ByUsername(string username) => 
        Entities.FirstOrDefault(user => user.Username == username);
}