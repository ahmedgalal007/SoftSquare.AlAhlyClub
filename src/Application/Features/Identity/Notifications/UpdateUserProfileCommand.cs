using SoftSquare.AlAhlyClub.Application.Common.Security;

namespace SoftSquare.AlAhlyClub.Application.Features.Identity.Notifications;

public class UpdateUserProfileCommand : INotification
{
    public UserProfile UserProfile { get; set; } = null!;
}

public class UpdateUserProfileEventArgs : EventArgs
{
    public UserProfile UserProfile { get; set; } = null!;
}