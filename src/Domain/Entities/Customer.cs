// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SoftSquare.AlAhlyClub.Domain.Common.Entities;

namespace SoftSquare.AlAhlyClub.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}