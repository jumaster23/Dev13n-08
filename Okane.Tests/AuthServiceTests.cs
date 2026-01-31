using Okane.Application.Auth;
using Okane.Storage.InMemory;

namespace Okane.Tests;

public class AuthServiceTests
{
    private AuthService _service;

    public AuthServiceTests()
    {
        _service = new AuthService(new InMemoryUsersRepository());
    }

    [Fact]
    public void SignUp()
    {
        var response = _service.SignUp(new("test-user", "1234"))
            .AssertOk();
        
        Assert.Equal("test-user", response.Username);
    }
    
    [Fact]
    public void SingIn()
    {
        _service.SignUp(new("test-user", "1234"))
            .AssertOk();
        
        var response = _service.SignIn(new("test-user", "1234"))
            .AssertOk();
        
        Assert.Equal("ASDFGH", response.Token);
    }
    
    [Fact]
    public void SingIn_PasswordDoesNotMatch()
    {
        _service.SignUp(new("test-user", "4321"))
            .AssertOk();
        
        var error = _service.SignIn(new("test-user", "1234"))
            .AssertUnauthorized();
        
        Assert.Equal("Invalid username or password.", error);
    }
    
    [Fact]
    public void SingIn_UserDoesNotExist()
    {
        var error = _service.SignIn(new("test-user", "1234"))
            .AssertUnauthorized();
        
        Assert.Equal("Invalid username or password.", error);
    }
}