using System.ComponentModel.DataAnnotations;

namespace Nexora.Api.Models.System.Users;

public class UserProfileAvatarModel
{
    [Required(ErrorMessage = "O campo Avatar é obrigatório.")]
    public IFormFile Avatar { get; set; } = default!;
}
