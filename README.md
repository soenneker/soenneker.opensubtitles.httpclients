[![](https://img.shields.io/nuget/v/soenneker.opensubtitles.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.opensubtitles.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.opensubtitles.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.opensubtitles.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.opensubtitles.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.opensubtitles.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.opensubtitles.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.opensubtitles.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.OpenSubtitles.HttpClients

Provides a cached `HttpClient` configured for the OpenSubtitles REST API, including application-key and optional user-token authentication.

## Installation

```bash
dotnet add package Soenneker.OpenSubtitles.HttpClients
```

## Configuration

```json
{
  "OpenSubtitles": {
    "ApiKey": "your-application-api-key",
    "Token": "your-user-token"
  }
}
```

`ApiKey` is required. `Token` is needed for endpoints that act as an authenticated user, including subtitle downloads. `ClientBaseUrl`, `ApiKeyHeaderName`, `AuthHeaderName`, and `AuthHeaderValueTemplate` can override their defaults under the same configuration section.

## Usage

```csharp
using Soenneker.OpenSubtitles.HttpClients.Abstract;
using Soenneker.OpenSubtitles.HttpClients.Registrars;

services.AddOpenSubtitlesOpenApiHttpClientAsSingleton();

IOpenSubtitlesOpenApiHttpClient provider = serviceProvider
    .GetRequiredService<IOpenSubtitlesOpenApiHttpClient>();

HttpClient client = await provider.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync(
    "subtitles?query=Arrival&languages=en",
    cancellationToken);
response.EnsureSuccessStatusCode();
```

The provider owns its cached client. Disposing the provider removes and disposes that client. Scoped registration gives each provider instance its own cached client.
