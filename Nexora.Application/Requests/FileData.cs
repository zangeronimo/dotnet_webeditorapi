namespace Nexora.Application.Requests;

public sealed record FileData(
Stream Stream,
string FileName,
string ContentType,
long Length);
