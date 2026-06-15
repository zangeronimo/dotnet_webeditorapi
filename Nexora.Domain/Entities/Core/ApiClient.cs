using Nexora.Domain.Enums;

namespace Nexora.Domain.Entities.Core;

public class ApiClient : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public Guid CompanyId { get; private set; }
    public string ClientId { get; private set; }
    public string EncryptedSecret { get; private set; }

    public ApiClient(string name, Status status, Guid companyId, string clientId, string encryptedSecret) : base()
    {
        Name = name;
        Status = status;
        CompanyId = companyId;
        ClientId = clientId;
        EncryptedSecret = encryptedSecret;
    }

    protected ApiClient() : base() { }

    public void Update(string newName, Status newStatus)
    {
        Name = newName;
        Status = newStatus;
        Touch();
    }
}
