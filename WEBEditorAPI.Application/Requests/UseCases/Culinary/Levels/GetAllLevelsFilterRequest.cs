using System;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.Requests.UseCases.Culinary.Levels;

public sealed record GetAllLevelsFilterRequest(
    int Page,
    int PageSize,
    string OrderBy,
    bool Desc,
    string? Name,
    Status? Active,
    RequestContext Context) : ApplicationRequest(Context);