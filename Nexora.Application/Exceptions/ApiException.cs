using Nexora.Domain.Errors.Core;

namespace Nexora.Application.Exceptions;

public abstract class ApiException(int statusCode, string key) : Exception(key)
{
    public string Key { get; } = key;
    public int StatusCode { get; } = statusCode;
}
public class ApiInvalidCredentialsException(string key = AuthErrors.InvalidCredentials) : ApiException(401, key)
{ }

public class ApiForbiddenException(string key = AuthErrors.AccessDenied) : ApiException(403, key)
{ }

public class ApiNotFoundException(string key) : ApiException(404, key)
{ }

public class ApiBadRequestException(string key) : ApiException(400, key)
{ }