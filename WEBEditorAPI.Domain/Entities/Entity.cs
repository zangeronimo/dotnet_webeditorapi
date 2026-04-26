namespace WEBEditorAPI.Domain.Entities { }

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset? UpdatedAt { get; protected set; }
    public DateTimeOffset? DeletedAt { get; protected set; }

    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Touch()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Delete()
    {
        DeletedAt = DateTimeOffset.UtcNow;
    }
}