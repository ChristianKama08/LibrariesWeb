using Serilog;

namespace LibrariesWeb.API.Extensions;

public static partial class ApplicationDependenciesConfiguration
{
    /// <summary>
    /// Configuring The logger 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IServiceCollection AddLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName();
        });

        return builder.Services;
    }
}