﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using SoftSquare.AlAhlyClub.Application.Common.Interfaces.MultiTenant;
using SoftSquare.AlAhlyClub.Application.Features.Tenants.Caching;
using SoftSquare.AlAhlyClub.Application.Features.Tenants.DTOs;
using LazyCache;
using ZiggyCreatures.Caching.Fusion;

namespace SoftSquare.AlAhlyClub.Infrastructure.Services.MultiTenant;

public class TenantService : ITenantService
{

    private readonly IApplicationDbContext _context;
    private readonly IFusionCache _fusionCache;
    private readonly IMapper _mapper;

    public TenantService(
        IFusionCache fusionCache,
        IServiceScopeFactory scopeFactory,
        IMapper mapper)
    {

        var scope = scopeFactory.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _fusionCache = fusionCache;
        _mapper = mapper;
    }

    public event Action? OnChange;
    public List<TenantDto> DataSource { get; private set; } = new();



    public void Initialize()
    {
        DataSource = _fusionCache.GetOrSet(TenantCacheKey.TenantsCacheKey,
            _ => _context.Tenants.OrderBy(x => x.Name)
                .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                .ToList()) ?? new List<TenantDto>();
    }

    public void Refresh()
    {
        _fusionCache.Remove(TenantCacheKey.TenantsCacheKey);
        DataSource = _fusionCache.GetOrSet(TenantCacheKey.TenantsCacheKey,
             _ => _context.Tenants.OrderBy(x => x.Name)
                 .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                 .ToList()) ?? new List<TenantDto>();
        OnChange?.Invoke();
    }
}