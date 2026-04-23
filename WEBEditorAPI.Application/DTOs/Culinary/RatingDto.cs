using System;
using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Application.DTOs.Culinary;

public class RatingDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Rate { get; set; }
    public string? Comment { get; set; }
    public Status Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
