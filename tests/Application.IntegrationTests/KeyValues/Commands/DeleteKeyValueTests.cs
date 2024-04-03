using System.Threading.Tasks;
using SoftSquare.AlAhlyClub.Application.Common.ExceptionHandlers;
using SoftSquare.AlAhlyClub.Application.Features.KeyValues.Commands.AddEdit;
using SoftSquare.AlAhlyClub.Application.Features.KeyValues.Commands.Delete;
using SoftSquare.AlAhlyClub.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace SoftSquare.AlAhlyClub.Application.IntegrationTests.KeyValues.Commands;

using static Testing;

public class DeleteKeyValueTests : TestBase
{
    [Test]
    public void ShouldRequireValidKeyValueId()
    {
        var command = new DeleteKeyValueCommand(new[] { 99 });

        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteKeyValue()
    {
        var addCommand = new AddEditKeyValueCommand
        {
            Name = Picklist.Brand,
            Text = "Word",
            Value = "Word",
            Description = "For Test"
        };
        var result = await SendAsync(addCommand);

        await SendAsync(new DeleteKeyValueCommand(new[] { result.Data }));

        var item = await FindAsync<Document>(result.Data);

        item.Should().BeNull();
    }
}