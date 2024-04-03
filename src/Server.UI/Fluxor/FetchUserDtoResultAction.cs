using SoftSquare.AlAhlyClub.Application.Common.Security;
using SoftSquare.AlAhlyClub.Application.Features.Identity.DTOs;

namespace SoftSquare.AlAhlyClub.Server.UI.Fluxor;

public class FetchUserDtoResultAction
{
    public FetchUserDtoResultAction(ApplicationUserDto dto)
    {
        UserProfile = new UserProfile
        {
            UserId = dto.Id,
            ProfilePictureDataUrl = dto.ProfilePictureDataUrl,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            DisplayName = dto.DisplayName,
            Provider = dto.Provider,
            UserName = dto.UserName,
            TenantId = dto.TenantId,
            TenantName = dto.TenantName,
            SuperiorId = dto.SuperiorId,
            SuperiorName = dto.SuperiorName,
            AssignedRoles = dto.AssignedRoles,
            DefaultRole = dto.DefaultRole
        };
    }

    public UserProfile UserProfile { get; }
}