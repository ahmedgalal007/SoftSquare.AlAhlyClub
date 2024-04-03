global using System.ComponentModel;
global using System.Data;
global using System.Globalization;
global using System.Linq.Dynamic.Core;
global using System.Linq.Expressions;
global using System.Reflection;
global using System.Text.Json;
global using Ardalis.Specification;
global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using SoftSquare.AlAhlyClub.Application.Common.ExceptionHandlers;
global using SoftSquare.AlAhlyClub.Application.Common.Extensions;
global using SoftSquare.AlAhlyClub.Application.Common.Interfaces;
global using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Caching;
global using SoftSquare.AlAhlyClub.Application.Common.Models;
global using SoftSquare.AlAhlyClub.Domain.Common.Enums;
global using SoftSquare.AlAhlyClub.Domain.Common.Events;
global using SoftSquare.AlAhlyClub.Domain.Events;
global using SoftSquare.AlAhlyClub.Domain.Entities;
global using FluentValidation;
global using LazyCache;
global using MediatR;
global using MediatR.Pipeline;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Primitives;