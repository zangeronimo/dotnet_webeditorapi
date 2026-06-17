using Nexora.Domain.Entities.System;
using Nexora.Domain.Enums;

namespace Nexora.Domain.Entities.Core;

public class Company : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public ICollection<UserCompany> Users { get; set; } = new List<UserCompany>();
    private readonly List<CompanyModule> _companyModules = [];
    public IReadOnlyCollection<CompanyModule> CompanyModules => _companyModules;
    private readonly List<ApiClient> _apiClients = [];
    public IReadOnlyCollection<ApiClient> ApiClients => _apiClients;

    public Company(string name, Status status) : base()
    {
        Name = name;
        Status = status;
    }

    protected Company() : base() { }

    public void Update(string newName, Status newStatus)
    {
        Name = newName;
        Status = newStatus;
        Touch();
    }

    public void SetModules(IEnumerable<Module> modules)
    {
        var newModuleIds = modules.Select(m => m.Id).ToHashSet();
        _companyModules.RemoveAll(m => !newModuleIds.Contains(m.ModuleId));
        foreach (var module in modules)
        {
            if (_companyModules.All(m => m.ModuleId != module.Id))
            {
                var companyModule = new CompanyModule() { ModuleId = module.Id, CompanyId = Id, Company = this, Module = module };
                _companyModules.Add(companyModule);
            }
        }
    }

    public void SetApiClients(IEnumerable<ApiClient> apiClients)
    {
        var newApiClientIds = apiClients.Select(x => x.Id).ToHashSet();

        foreach (var apiClient in _apiClients)
        {
            if (!newApiClientIds.Contains(apiClient.Id))
            {
                apiClient.Delete();
                continue;
            }

            var updatedApiClient = apiClients.First(x => x.Id == apiClient.Id);
            apiClient.Update(updatedApiClient.Name, updatedApiClient.Status);
        }

        foreach (var apiClient in apiClients)
        {
            if (_apiClients.All(x => x.Id != apiClient.Id))
            {
                _apiClients.Add(apiClient);
            }
        }
    }
}
