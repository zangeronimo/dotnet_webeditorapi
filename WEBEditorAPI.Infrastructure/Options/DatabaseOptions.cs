namespace WEBEditorAPI.Infrastructure.Options;

public class DatabaseOptions
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Name { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string ConnectionString =>
        $"Host={Host};Port={Port};Database={Name};Username={User};Password={Password}";
}
