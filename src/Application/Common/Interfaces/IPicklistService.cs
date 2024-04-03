using SoftSquare.AlAhlyClub.Application.Features.KeyValues.DTOs;

namespace SoftSquare.AlAhlyClub.Application.Common.Interfaces;

public interface IPicklistService
{
    List<KeyValueDto> DataSource { get; }
    event Action? OnChange;
    void Initialize();
    void Refresh();
}