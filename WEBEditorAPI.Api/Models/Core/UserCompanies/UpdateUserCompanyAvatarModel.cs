using System;
using System.ComponentModel.DataAnnotations;

namespace WEBEditorAPI.Api.Models.Core.UserCompanies;

public class UpdateUserCompanyAvatarModel
{
    [Required(ErrorMessage = "O campo Avatar é obrigatório.")]
    public IFormFile Avatar { get; set; } = default!;
}
