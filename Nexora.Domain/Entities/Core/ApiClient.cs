using Nexora.Domain.Enums;
using Nexora.Domain.Errors.System;
using Nexora.Domain.Exceptions;

namespace Nexora.Domain.Entities.Core;

public class ApiClient : Entity
{
    public string Name { get; private set; } = null!;
    public ApiClientStatus Status { get; private set; }
    public Guid CompanyId { get; private set; }
    public string ClientId { get; private set; } = null!;
    public string? EncryptedSecret { get; private set; } = null!;

    public ApiClient(string name, ApiClientStatus status, Guid companyId, string? clientId, string? encryptedSecret) : base()
    {
        Name = name;
        Status = status;
        CompanyId = companyId;
        ClientId = clientId;
        EncryptedSecret = encryptedSecret;
    }

    protected ApiClient() : base() { }

    public void Update(string newName, ApiClientStatus newStatus)
    {
        Name = newName;
        Status = newStatus;
        Touch();
    }

    public void SetEncryptedSecret(string encriptedSecret)
    {
        EncryptedSecret = encriptedSecret;
        Touch();
    }

    public void SetActive()
    {
        if (Status == ApiClientStatus.Revoked)
        {
            throw new DomainException(ApiClientErrors.Revoked);
        }
        Status = ApiClientStatus.Active;
        Touch();
    }

    public void SetInactive()
    {
        if (Status == ApiClientStatus.Revoked)
        {
            throw new DomainException(ApiClientErrors.Revoked);
        }
        Status = ApiClientStatus.Inactive;
    }
}
