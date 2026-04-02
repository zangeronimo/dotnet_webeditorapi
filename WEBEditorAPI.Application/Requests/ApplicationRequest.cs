using WEBEditorAPI.Application.DTOs;

namespace WEBEditorAPI.Application.Requests;

public abstract record ApplicationRequest(
    RequestContext Context
);
