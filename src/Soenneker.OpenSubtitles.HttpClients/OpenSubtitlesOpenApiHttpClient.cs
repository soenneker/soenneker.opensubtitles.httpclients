using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.OpenSubtitles.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.OpenSubtitles.HttpClients;

/// <inheritdoc cref="IOpenSubtitlesOpenApiHttpClient" />
public sealed class OpenSubtitlesOpenApiHttpClient : IOpenSubtitlesOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _clientId = $"{nameof(OpenSubtitlesOpenApiHttpClient)}:{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://api.opensubtitles.com/api/v1/";

    public OpenSubtitlesOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, (config: _config, baseUrl: _config["OpenSubtitles:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("OpenSubtitles:ApiKey");
            string apiKeyHeaderName = state.config["OpenSubtitles:ApiKeyHeaderName"] ?? "Api-Key";
            string? token = state.config["OpenSubtitles:Token"];
            string authHeaderName = state.config["OpenSubtitles:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["OpenSubtitles:AuthHeaderValueTemplate"] ?? "Bearer {token}";

            var headers = new Dictionary<string, string>
            {
                {apiKeyHeaderName, apiKey},
            };

            if (!string.IsNullOrWhiteSpace(token))
                headers.Add(authHeaderName, authHeaderValueTemplate.Replace("{token}", token, StringComparison.Ordinal));

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = headers
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}
