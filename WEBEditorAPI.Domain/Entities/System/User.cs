using System;
using WEBEditorAPI.Domain.ValueObjects;

namespace WEBEditorAPI.Domain.Entities.System;

public class User : Entity
{
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public Guid CompanyId { get; private set; }

    public User(string name, Email email, Password password, Guid companyId) : base()
    {
        Name = name;
        Email = email;
        Password = password;
        CompanyId = companyId;
    }

    protected User() : base() { }

    public void Update(string newName, Email newEmail, Password newPassword)
    {
        Name = newName;
        Email = newEmail;
        Password = newPassword;
        Touch();
    }
}
