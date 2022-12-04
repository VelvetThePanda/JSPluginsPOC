using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Remora.Discord.Rest;
using Remora.Rest.Core;
using Tenray.Topaz;

namespace JSPluginsPOC;

public class PluginManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Snowflake, IServiceProvider> _plugins = new();
    
    public void InitializeEngineForGuild(Snowflake guildId)
    {
        var engine = new TopazEngine();
        var services = new ServiceCollection();

        services.AddSingleton(engine);
        services.AddSingleton(new SnowflakeStore(guildId));
        _plugins.Add(guildId, services.BuildServiceProvider());
    }

    public async Task InitializePluginsForGuildAsync(Snowflake guildID)
    {
        if (!_plugins.TryGetValue(guildID, out var serviceProvider))
        {
            throw new InvalidOperationException("Guild not initialized");
        }
        
        var engine = serviceProvider.GetRequiredService<TopazEngine>();
        
    }
}