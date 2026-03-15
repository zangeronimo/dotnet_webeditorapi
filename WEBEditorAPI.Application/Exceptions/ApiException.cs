namespace WEBEditorAPI.Application.Exceptions;

public abstract class ApiException : Exception
{
    public ApiException(string message) : base(message) { }
}
public class ApiInvalidCredentialsException : ApiException
{
    public ApiInvalidCredentialsException(string message = "Usuário ou Senha inválido")
        : base(message) { }
}

public class ApiForbiddenException : ApiException
{
    public ApiForbiddenException(string message = "Acesso negado")
        : base(message) { }
}

public class ApiNotFoundException : ApiException
{
    public ApiNotFoundException(string message = "Recurso não encontrado")
        : base(message) { }
}