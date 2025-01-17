// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SoftSquare.AlAhlyClub.Application.Common.Interfaces;

public interface IResult
{
    string[] Errors { get; init; }

    bool Succeeded { get; init; }
}

public interface IResult<out T> : IResult
{
    T? Data { get; }
}