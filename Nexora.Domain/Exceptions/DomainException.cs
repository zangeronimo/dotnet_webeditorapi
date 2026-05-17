
namespace Nexora.Domain.Exceptions;

public class DomainException(string key) : Exception(key)
{ }