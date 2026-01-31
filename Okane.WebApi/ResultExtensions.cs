using Okane.Application;

namespace Okane.WebApi;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(this Result<T> result) =>
        result switch
        {
            ErrorResult<T> errorResult => Results.BadRequest(errorResult),
            NotFoundResult<T> notFoundResult => Results.NotFound(notFoundResult),
            OkResult<T> okResult => Results.Ok(okResult.Value),
            UnauthorizedResult<T> => Results.Unauthorized(),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    
    public static IResult ToHttpResult(this Result result) =>
        result switch
        {
            ErrorResult errorResult => Results.BadRequest(errorResult),
            NotFoundResult notFoundResult => Results.NotFound(notFoundResult),
            OkResult => Results.Ok(),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
}