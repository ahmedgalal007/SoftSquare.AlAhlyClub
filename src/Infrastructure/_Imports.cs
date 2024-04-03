// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

global using System.Security.Claims;
global using SoftSquare.AlAhlyClub.Application.Common.Interfaces;
global using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Identity;
global using SoftSquare.AlAhlyClub.Application.Common.Models;
global using SoftSquare.AlAhlyClub.Infrastructure.Persistence;
global using SoftSquare.AlAhlyClub.Infrastructure.Persistence.Extensions;
global using SoftSquare.AlAhlyClub.Infrastructure.Services;
global using SoftSquare.AlAhlyClub.Infrastructure.Services.Identity;
global using SoftSquare.AlAhlyClub.Domain.Entities;
global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;