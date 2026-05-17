using System;
using Nexora.Domain.Enums;

namespace Nexora.Application.Requests.UseCases.Culinary.Levels;

public sealed record GetAllLevelsFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Active,
    RequestContext Context) : ApplicationRequest(Context);