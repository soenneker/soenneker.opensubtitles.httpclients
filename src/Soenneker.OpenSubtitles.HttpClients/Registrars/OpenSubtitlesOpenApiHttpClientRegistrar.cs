using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.OpenSubtitles.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.OpenSubtitles.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class OpenSubtitlesOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="OpenSubtitlesOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddOpenSubtitlesOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IOpenSubtitlesOpenApiHttpClient, OpenSubtitlesOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="OpenSubtitlesOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddOpenSubtitlesOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IOpenSubtitlesOpenApiHttpClient, OpenSubtitlesOpenApiHttpClient>();

        return services;
    }
}
