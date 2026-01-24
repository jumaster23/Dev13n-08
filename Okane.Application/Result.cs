namespace Okane.Application;

public abstract record Result;

public record ErrorResult(string Message) : Result;

public record OkResult() : Result;

public record NotFoundResult(string Message) : Result;

public abstract record Result<T>;

public record ErrorResult<T>(string Message) : Result<T>;

public record OkResult<T>(T Value) : Result<T>;

public record NotFoundResult<T>(string Message) : Result<T>;