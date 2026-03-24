using Soenneker.OpenSubtitles.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.OpenSubtitles.HttpClients.Tests;

[Collection("Collection")]
public sealed class OpenSubtitlesOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IOpenSubtitlesOpenApiHttpClient _httpclient;

    public OpenSubtitlesOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IOpenSubtitlesOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
