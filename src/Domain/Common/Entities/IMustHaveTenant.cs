namespace SoftSquare.AlAhlyClub.Domain.Common.Entities;

public interface IMustHaveTenant
{
    string TenantId { get; set; }
}

public interface IMayHaveTenant
{
    string? TenantId { get; set; }
}