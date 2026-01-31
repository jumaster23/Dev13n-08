using Okane.Domain;

namespace Okane.Application.Auth;

public class AuthService(IUsersRepository users)
{
    public Result<SignUpResponse> SignUp(SignUpRequest request)
    {
        users.Add(new User
        {
            Username = request.Username,
            Password = request.Password,
        });
        
        return new OkResult<SignUpResponse>(new SignUpResponse(request.Username));
    }

    public Result<SignInResponse> SignIn(SignInRequest request)
    {
        var username = users.ByUsername(request.Username);
        
        if (username == null)
            return new UnauthorizedResult<SignInResponse>("Invalid username or password.");
        
        if (username.Password != request.Password)
            return new UnauthorizedResult<SignInResponse>("Invalid username or password.");
        
        return new OkResult<SignInResponse>(new SignInResponse("ASDFGH"));
    }
}