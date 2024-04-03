// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SoftSquare.AlAhlyClub.Domain.Common.Entities;
using SoftSquare.AlAhlyClub.Domain.Common.Enums;

namespace SoftSquare.AlAhlyClub.Domain.Entities;

public class Document : OwnerPropertyEntity, IMayHaveTenant, IAuditTrial
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public JobStatus Status { get; set; } = default!;
    public string? Content { get; set; }
    public bool IsPublic { get; set; }
    public string? URL { get; set; }
    public DocumentType DocumentType { get; set; } = default!;
    public virtual Tenant? Tenant { get; set; }
    public string? TenantId { get; set; }
}

public enum DocumentType
{
    Document,
    Excel,
    Image,
    PDF,
    Others
}