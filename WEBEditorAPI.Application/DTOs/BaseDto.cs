using System;

namespace WEBEditorAPI.Application.DTOs;

public abstract class BaseDto
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
