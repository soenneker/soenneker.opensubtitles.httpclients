using Soenneker.OpenSubtitles.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.OpenSubtitles.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class OpenSubtitlesOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IOpenSubtitlesOpenApiHttpClient _httpclient;

    public OpenSubtitlesOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IOpenSubtitlesOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
