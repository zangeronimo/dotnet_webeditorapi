namespace WEBEditorAPI.Application.Exceptions;

public abstract class ApiException(int statusCode, string message) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}
public class ApiInvalidCredentialsException(string message = "Usuário ou Senha inválido") : ApiException(401, message)
{ }

public class ApiForbiddenException(string message = "Acesso negado") : ApiException(403, message)
{ }

public class ApiNotFoundException(string message = "Recurso não encontrado") : ApiException(404, message)
{ }

public class ApiBadRequestException(string message = "Falha no processamento") : ApiException(400, message)
{ }