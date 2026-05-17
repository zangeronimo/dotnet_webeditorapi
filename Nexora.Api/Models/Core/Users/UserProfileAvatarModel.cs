using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.Core.Users;

public class UserProfileAvatarModel
{
    [Required(ErrorMessage = "O campo Avatar é obrigatório.")]
    public IFormFile Avatar { get; set; } = default!;
}
