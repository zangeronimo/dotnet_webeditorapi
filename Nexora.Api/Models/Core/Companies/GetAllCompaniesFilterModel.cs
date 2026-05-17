using System;
using Nexora.Domain.Enums;

namespace Nexora.Api.Models.Core.Companies;

public class GetAllCompaniesFilterModel : PaginationModel
{
    public string OrderBy { get; init; } = "Id";
    public bool Desc { get; init; } = false;
    public string? Name { get; init; }
    public Status? Status { get; init; }
}