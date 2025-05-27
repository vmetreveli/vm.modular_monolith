using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace vm.modular.Api;

public static class RegisterSerilogServices
{
    /// <summary>
    ///     Register the Serilog service with a custom configuration.
    /// </summary>
    private static IServiceCollection AddSerilogServices(this IServiceCollection services,
        LoggerConfiguration configuration)
    {
        Log.Logger = configuration.CreateLogger();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
        return services.AddSingleton(Log.Logger);
    }


    public static IServiceCollection AddSerilogServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddSerilogServices(
            new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
        );
    }
}