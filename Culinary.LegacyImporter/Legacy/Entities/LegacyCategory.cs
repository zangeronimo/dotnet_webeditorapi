using Nexora.Domain.Enums;
using Nexora.Domain.ValueObjects;

namespace Culinary.LegacyImporter.Legacy.Entities;

public class LegacyCategory
{
    public Guid Id { get; set; }
    public Slug Slug { get; set; }
    public string Name { get; set; }
    public Status Active { get; set; }
    public Guid LevelId { get; set; }
    public Guid CompanyId { get; set; }
}

