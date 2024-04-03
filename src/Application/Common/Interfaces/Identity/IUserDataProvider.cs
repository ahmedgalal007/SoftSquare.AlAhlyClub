using SoftSquare.AlAhlyClub.Application.Features.Identity.DTOs;

namespace SoftSquare.AlAhlyClub.Application.Common.Interfaces.Identity;

public interface IUserService
{
    List<ApplicationUserDto> DataSource { get; }
    event Action? OnChange;
    void Initialize();
    void Refresh();
}