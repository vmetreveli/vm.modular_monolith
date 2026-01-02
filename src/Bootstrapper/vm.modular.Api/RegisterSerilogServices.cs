using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace vm.modular.Api;

public static class RegisterSerilogServices
{
    /// <summary>
    /// Register the Serilog service with a custom configuration.
    /// </summary>
    private static IServiceCollection AddSerilogServices(this IServiceCollection services,
        LoggerConfiguration configuration)
    {
        Log.Logger = configuration.CreateLogger();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
        return services.AddSingleton(Log.Logger);
    }

    /// <summary>
    /// Register Serilog with configuration and Elasticsearch sink.
    /// </summary>
    public static IServiceCollection AddSerilogServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var elasticUri = configuration["Elastic:Uri"];
        var elasticIndex = configuration["Elastic:Index"] ?? "modular-monolith-logs";
        var elasticUser = configuration["Elastic:Username"];
        var elasticPass = configuration["Elastic:Password"];

        var loggerConfig = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Console();

        if (!string.IsNullOrEmpty(elasticUri))
        {
            loggerConfig.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = elasticIndex + "-{0:yyyy.MM.dd}",
                ModifyConnectionSettings = x => x.BasicAuthentication(elasticUser, elasticPass)
            });
        }

        return services.AddSerilogServices(loggerConfig);
    }
}