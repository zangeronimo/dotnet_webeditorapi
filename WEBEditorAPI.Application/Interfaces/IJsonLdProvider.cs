namespace WEBEditorAPI.Application.Interfaces;

public interface IJsonLdProvider<TInput, TOutput>
{
    TOutput Generate(TInput data);
}
