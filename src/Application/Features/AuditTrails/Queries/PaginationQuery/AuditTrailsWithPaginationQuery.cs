// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SoftSquare.AlAhlyClub.Application.Features.AuditTrails.Caching;
using SoftSquare.AlAhlyClub.Application.Features.AuditTrails.DTOs;
using SoftSquare.AlAhlyClub.Application.Features.AuditTrails.Specifications;

namespace SoftSquare.AlAhlyClub.Application.Features.AuditTrails.Queries.PaginationQuery;

public class AuditTrailsWithPaginationQuery : AuditTrailAdvancedFilter, ICacheableRequest<PaginatedData<AuditTrailDto>>
{
    public AuditTrailAdvancedSpecification Specification => new(this);

    public string CacheKey => AuditTrailsCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => AuditTrailsCacheKey.MemoryCacheEntryOptions;

    public override string ToString()
    {
        return
            $"Listview:{ListView},AuditType:{AuditType},Search:{Keyword},Sort:{SortDirection},OrderBy:{OrderBy},{PageNumber},{PageSize}";
    }
}

public class AuditTrailsQueryHandler : IRequestHandler<AuditTrailsWithPaginationQuery, PaginatedData<AuditTrailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public AuditTrailsQueryHandler(
        ICurrentUserService currentUserService,
        IApplicationDbContext context,
        IMapper mapper
    )
    {
        _currentUserService = currentUserService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedData<AuditTrailDto>> Handle(AuditTrailsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _context.AuditTrails.OrderBy($"{request.OrderBy} {request.SortDirection}")
            .ProjectToPaginatedDataAsync<AuditTrail, AuditTrailDto>(request.Specification, request.PageNumber,
                request.PageSize, _mapper.ConfigurationProvider, cancellationToken);

        return data;
    }
}