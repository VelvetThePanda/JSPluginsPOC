using System.Reflection;
using Esprima.Ast;
using Microsoft.Extensions.DependencyInjection;
using Remora.Commands.Builders;
using Remora.Results;
using Tenray.Topaz;
using Tenray.Topaz.API;

namespace JSPluginsPOC;

public class GuildPlugin
{
    private const string PluginsDirectory = "GuildPlugins/";
    private const string InitializeMethodName = "onLoad";

    private readonly string _pluginName;
    private readonly IServiceCollection _serviceCollection;
    private readonly ITopazEngineScope _engine;
    private readonly JsObject _configuration;

    private IServiceProvider _services;
    
    public GuildPlugin(string name, IServiceCollection serviceCollection, ITopazEngineScope engine)
    {
        _serviceCollection = serviceCollection;
        _engine = engine;
        _configuration = new();
    }

    public async Task<Result> LoadAsync()
    {
        var source = File.ReadAllText($"{PluginsDirectory}{_pluginName}.js");

        var ct = new CancellationTokenSource(TimeSpan.FromSeconds(1));

        try
        {
            await _engine.ExecuteExpressionAsync(source, ct.Token);
            await _engine.InvokeFunctionAsync(InitializeMethodName, ct.Token, _configuration);
        }
        catch (TopazException te) when (te.Message is $"Function {InitializeMethodName} is not defined.")
        {
            // Ignore; the plugin doesn't have an onLoad method.
        }
        catch (OperationCanceledException)
        {
            return Result.FromError(new InvalidOperationError("Plugin failed to initialize within the timeout period."));
        }
        catch (Exception e)
        {
            return e;
        }
        
        var commands = _engine.GetValue("expots.commands") as JsObject;

        if (commands is not null)
        {
            InitializeCommands(commands);
        }

        return Result.FromSuccess();
    }

    private void InitializeCommands(JsObject commands)
    {
        foreach (var obj in commands)
        {
            if (obj is not ITopazFunction func)
            {
                continue;
            }
            
            var builder = new CommandBuilder();
            
            
            
        }
    }

    
}