using WEBEditorAPI.Domain.Enums;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Domain.Entities.Core;

public class User : Entity
{
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password PasswordHash { get; private set; } = null!;
    public Status Status { get; private set; }
    public ICollection<UserCompany> Companies { get; set; } = [];

    public User(string name, Email email, Password password, Status status) : base()
    {
        Name = name;
        Email = email;
        PasswordHash = password;
        Status = status;
    }

    protected User() : base() { }

    public void Update(string newName, Email newEmail, Status newStatus)
    {
        Name = newName;
        Email = newEmail;
        Status = newStatus;
        Touch();
    }

    public void UpdatePassword(Password newPassword)
    {
        PasswordHash = newPassword;
        Touch();
    }
}
