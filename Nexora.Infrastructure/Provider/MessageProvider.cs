using Microsoft.Extensions.Localization;
using Nexora.Domain.Interfaces.Provider;
using Nexora.Infrastructure.Resources;

namespace Nexora.Infrastructure.Provider;

public class MessageProvider : IMessageProvider
{
    private readonly IStringLocalizer _localizer;

    public MessageProvider(IStringLocalizerFactory factory)
    {
        var type = typeof(Messages);
        var assemblyName = type.Assembly.GetName().Name!;

        _localizer = factory.Create("Messages", assemblyName);
    }

    public string Get(string key)
    {
        var value = _localizer[key];
        return value.ResourceNotFound ? key : value.Value;
    }
}
