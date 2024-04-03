using SoftSquare.AlAhlyClub.Server.Common.Interfaces;
using SoftSquare.AlAhlyClub.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace SoftSquare.AlAhlyClub.Server.Services;

public class ServerHubWrapper : IApplicationHubWrapper
{
    private readonly IHubContext<ServerHub, ISignalRHub> _hubContext;

    public ServerHubWrapper(IHubContext<ServerHub, ISignalRHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task JobStarted(string message)
    {
        await _hubContext.Clients.All.Start(message);
    }

    public async Task JobCompleted(string message)
    {
        await _hubContext.Clients.All.Completed(message);
    }
}