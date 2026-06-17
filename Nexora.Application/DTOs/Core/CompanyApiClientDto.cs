namespace Nexora.Application.DTOs.Core;

public class CompanyApiClientDto
{
    public CompanyDto Company { get; set; }
    public List<ApiClientDto> ApiClients { get; set; } = new List<ApiClientDto>();
}
