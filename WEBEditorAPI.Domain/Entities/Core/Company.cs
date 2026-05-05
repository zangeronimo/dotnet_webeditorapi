using WEBEditorAPI.Domain.Enums;

namespace WEBEditorAPI.Domain.Entities.Core;

public class Company : Entity
{
    public string Name { get; private set; } = null!;
    public Status Status { get; private set; }
    public ICollection<UserCompany> Users { get; set; } = new List<UserCompany>();
    private readonly List<Module> _modules = [];
    public IReadOnlyCollection<Module> Modules => _modules;

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
        var newModules = modules.ToList();
        _modules.RemoveAll(m => !newModules.Any(n => n.Id == m.Id));
        foreach (var module in newModules)
        {
            if (_modules.All(m => m.Id != module.Id))
            {
                _modules.Add(module);
            }
        }
    }
}
