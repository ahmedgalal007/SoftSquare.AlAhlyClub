using System.Threading.Tasks;
using SoftSquare.AlAhlyClub.Application.Features.KeyValues.Queries.Export;
using FluentAssertions;
using NUnit.Framework;

namespace SoftSquare.AlAhlyClub.Application.IntegrationTests.KeyValues.Queries;

using static Testing;

internal class ExportKeyValuesQueryTests : TestBase
{
    [Test]
    public async Task ShouldNotEmptyExportQuery()
    {
        var query = new ExportKeyValuesQuery();
        var result = await SendAsync(query);
        result.Should().NotBeNull();
    }
}