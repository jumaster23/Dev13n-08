using Okane.Application.Auth;

namespace Okane.Storage.InMemory;

public class FakePasswordHasher : IPasswordHasher
{
    public string Hash(string plainPassword) => plainPassword + "Hashed";
    public bool Verify(string plainPassword, string hashedPassword) => 
        plainPassword + "Hashed" == hashedPassword;
}