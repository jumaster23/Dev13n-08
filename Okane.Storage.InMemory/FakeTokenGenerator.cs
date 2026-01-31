using Okane.Application.Auth;
using Okane.Domain;

namespace Okane.Storage.InMemory;

public class FakeTokenGenerator : ITokenGenerator
{
    public string Generate(User user) => 
        "token-" + user.Username;
}