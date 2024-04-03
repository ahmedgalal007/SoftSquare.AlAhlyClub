using System.Threading.Tasks;
using NUnit.Framework;

namespace SoftSquare.AlAhlyClub.Application.IntegrationTests;

using static Testing;

public class TestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}