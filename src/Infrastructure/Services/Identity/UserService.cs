using AutoMapper;
using AutoMapper.QueryableExtensions;
using SoftSquare.AlAhlyClub.Application.Features.Identity.DTOs;
using SoftSquare.AlAhlyClub.Domain.Identity;
using LazyCache;
using ZiggyCreatures.Caching.Fusion;

namespace SoftSquare.AlAhlyClub.Infrastructure.Services.Identity;

public class UserService : IUserService
{
    private const string CACHEKEY = "ALL-ApplicationUserDto";
    private readonly IFusionCache _fusionCache;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(
        IFusionCache fusionCache,
        IMapper mapper,
        IServiceScopeFactory scopeFactory)
    {
        _fusionCache = fusionCache;
        _mapper = mapper;
        var scope = scopeFactory.CreateScope();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        DataSource = new List<ApplicationUserDto>();
    }

    public List<ApplicationUserDto> DataSource { get; private set; }

    public event Action? OnChange;

    public void Initialize()
    {
        DataSource = _fusionCache.GetOrSet(CACHEKEY,
            _ => _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider).OrderBy(x => x.UserName).ToList())
            ?? new List<ApplicationUserDto>();
        OnChange?.Invoke();
    }

    

    public void Refresh()
    {
        _fusionCache.Remove(CACHEKEY);
        DataSource = _fusionCache.GetOrSet(CACHEKEY,
             _ => _userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                 .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider).OrderBy(x => x.UserName).ToList())
             ?? new List<ApplicationUserDto>();
        OnChange?.Invoke();
    }
}