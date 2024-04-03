// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SoftSquare.AlAhlyClub.Domain.Common.Entities;
using SoftSquare.AlAhlyClub.Domain.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SoftSquare.AlAhlyClub.Domain.Entities;

public class AuditTrail : IEntity<int>
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public virtual ApplicationUser? Owner { get; set; }
    public AuditType AuditType { get; set; }
    public string? TableName { get; set; }
    public DateTime DateTime { get; set; }
    public Dictionary<string, object?>? OldValues { get; set; }
    public Dictionary<string, object?>? NewValues { get; set; }
    public List<string>? AffectedColumns { get; set; }
    public Dictionary<string, object> PrimaryKey { get; set; } = new();
    public List<PropertyEntry> TemporaryProperties { get; } = new();
    public bool HasTemporaryProperties => TemporaryProperties.Any();
}

public enum AuditType
{
    None,
    Create,
    Update,
    Delete
}