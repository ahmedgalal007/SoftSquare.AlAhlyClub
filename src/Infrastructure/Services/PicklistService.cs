﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using SoftSquare.AlAhlyClub.Application.Features.KeyValues.Caching;
using SoftSquare.AlAhlyClub.Application.Features.KeyValues.DTOs;
using LazyCache;
using ZiggyCreatures.Caching.Fusion;

namespace SoftSquare.AlAhlyClub.Infrastructure.Services;

public class PicklistService : IPicklistService
{
    private readonly IApplicationDbContext _context;
    private readonly IFusionCache _fusionCache;
    private readonly IMapper _mapper;

    public PicklistService(
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
    public List<KeyValueDto> DataSource { get; private set; } = new();



    public void Initialize()
    {
        DataSource = _fusionCache.GetOrSet(KeyValueCacheKey.PicklistCacheKey,
            _ => _context.KeyValues.OrderBy(x => x.Name).ThenBy(x => x.Value)
                .ProjectTo<KeyValueDto>(_mapper.ConfigurationProvider)
                .ToList()
                )??new List<KeyValueDto>();
    }

    public void Refresh()
    {
        _fusionCache.Remove(KeyValueCacheKey.PicklistCacheKey);
        DataSource = _fusionCache.GetOrSet(KeyValueCacheKey.PicklistCacheKey,
             _ => _context.KeyValues.OrderBy(x => x.Name).ThenBy(x => x.Value)
                 .ProjectTo<KeyValueDto>(_mapper.ConfigurationProvider)
                 .ToList()
                 ) ?? new List<KeyValueDto>();
        OnChange?.Invoke();
    }
}