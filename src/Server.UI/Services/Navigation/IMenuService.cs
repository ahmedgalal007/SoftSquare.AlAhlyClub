using SoftSquare.AlAhlyClub.Server.UI.Models.NavigationMenu;

namespace SoftSquare.AlAhlyClub.Server.UI.Services.Navigation;

public interface IMenuService
{
    IEnumerable<MenuSectionModel> Features { get; }
}