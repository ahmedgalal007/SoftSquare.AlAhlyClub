using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Identity;

namespace SoftSquare.AlAhlyClub.Server.UI.Fluxor;

public class Effects
{
    private readonly IIdentityService _identityService;

    public Effects(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [EffectMethod]
    public async Task HandleFetchDataAction(FetchUserDtoAction action, IDispatcher dispatcher)
    {
        var result = await _identityService.GetApplicationUserDto(action.UserName);
        if (result is not null)
            dispatcher.Dispatch(new FetchUserDtoResultAction(result));
    }
}