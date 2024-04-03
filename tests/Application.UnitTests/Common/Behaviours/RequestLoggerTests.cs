using System.Threading;
using System.Threading.Tasks;
using SoftSquare.AlAhlyClub.Application.Common.Interfaces;
using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Identity;
using SoftSquare.AlAhlyClub.Application.Features.Products.Commands.AddEdit;
using SoftSquare.AlAhlyClub.Application.Pipeline.PreProcessors;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace SoftSquare.AlAhlyClub.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private readonly Mock<ICurrentUserService> _currentUserService;
    private readonly Mock<IIdentityService> _identityService;
    private readonly Mock<ILogger<AddEditProductCommand>> _logger;

    public RequestLoggerTests()
    {
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
        _logger = new Mock<ILogger<AddEditProductCommand>>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns("Administrator");
        var requestLogger = new LoggingPreProcessor<AddEditProductCommand>(_logger.Object, _currentUserService.Object);
        await requestLogger.Process(
            new AddEditProductCommand { Brand = "Brand", Name = "Brand", Price = 1.0m, Unit = "EA" },
            new CancellationToken());
        _currentUserService.Verify(i => i.UserName, Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingPreProcessor<AddEditProductCommand>(_logger.Object, _currentUserService.Object);
        await requestLogger.Process(
            new AddEditProductCommand { Brand = "Brand", Name = "Brand", Price = 1.0m, Unit = "EA" },
            new CancellationToken());
        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>(), CancellationToken.None), Times.Never);
    }
}