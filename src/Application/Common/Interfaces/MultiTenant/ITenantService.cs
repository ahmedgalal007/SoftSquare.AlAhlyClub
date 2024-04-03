using SoftSquare.AlAhlyClub.Application.Features.Tenants.DTOs;

namespace SoftSquare.AlAhlyClub.Application.Common.Interfaces.MultiTenant;

public interface ITenantService
{
    List<TenantDto> DataSource { get; }
    event Action? OnChange;
    void Initialize();
    void Refresh();
}