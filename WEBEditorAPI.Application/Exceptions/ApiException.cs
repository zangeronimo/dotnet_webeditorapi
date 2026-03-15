namespace WEBEditorAPI.Application.Exceptions;

public abstract class ApiException : Exception
{
    public int StatusCode { get; }
    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
public class ApiInvalidCredentialsException : ApiException
{
    public ApiInvalidCredentialsException(string message = "Usuário ou Senha inválido")
        : base(401, message) { }
}

public class ApiForbiddenException : ApiException
{
    public ApiForbiddenException(string message = "Acesso negado")
        : base(403, message) { }
}

public class ApiNotFoundException : ApiException
{
    public ApiNotFoundException(string message = "Recurso não encontrado")
        : base(404, message) { }
}