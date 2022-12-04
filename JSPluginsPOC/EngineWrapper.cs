using Microsoft.Extensions.DependencyInjection;
using Tenray.Topaz;

namespace JSPluginsPOC;

public class EngineWrapper
{
    private readonly ITopazEngine _engine;
    private readonly IServiceCollection _collection;
    private IServiceProvider _provider;
    
    public EngineWrapper()
    {
        _engine = new TopazEngine();
        _collection = new ServiceCollection();
    }
    
    

}