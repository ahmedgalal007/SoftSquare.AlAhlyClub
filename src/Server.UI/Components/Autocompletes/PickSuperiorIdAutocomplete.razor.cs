using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Identity;
using SoftSquare.AlAhlyClub.Application.Features.Identity.DTOs;

namespace SoftSquare.AlAhlyClub.Server.UI.Components.Autocompletes;

public class PickSuperiorIdAutocomplete : MudAutocomplete<string>
{
    private List<ApplicationUserDto>? _userList;
    [Parameter] public string? TenantId { get; set; }
    [Parameter] public string OwnerName { get; set; } = string.Empty;

    [Inject] private IIdentityService IdentityService { get; set; } = default!;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        SearchFuncWithCancel = SearchKeyValues;
        ToStringFunc = ToString;
        Clearable = true;
        Dense = true;
        ResetValueOnEmptyText = true;
        ShowProgressIndicator = true;
        MaxItems = 50;
        return base.SetParametersAsync(parameters);
    }

    private async Task<IEnumerable<string>> SearchKeyValues(string value, CancellationToken cancellation)
    {
        // if text is null or empty, show complete list
        _userList = await IdentityService.GetUsers(TenantId, cancellation);
        List<string> result = new();

        if (string.IsNullOrEmpty(value) && _userList is not null)
        {
            result = _userList.Select(x => x.Id).Take(MaxItems ?? 50).ToList();
        }
        else if (_userList is not null)
        {
            result = _userList
                .Where(x => !x.UserName.Equals(OwnerName, StringComparison.OrdinalIgnoreCase) &&
                            (x.UserName.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                             x.Email.Contains(value, StringComparison.OrdinalIgnoreCase))).Select(x => x.Id)
                .Take(MaxItems ?? 50).ToList();
            ;
        }

        return result;
    }

    private string ToString(string str)
    {
        if (_userList is not null && !string.IsNullOrEmpty(str) &&
            _userList.Any(x => x.Id.Equals(str, StringComparison.OrdinalIgnoreCase)))
        {
            var userDto = _userList.First(x => x.Id == str);
            return userDto.UserName;
        }

        if (_userList is null && !string.IsNullOrEmpty(str))
        {
            var userName = IdentityService.GetUserName(str);
            return userName;
        }

        return string.Empty;
    }
}