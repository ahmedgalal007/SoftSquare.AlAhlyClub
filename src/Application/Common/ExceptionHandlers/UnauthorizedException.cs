using System.Net;

namespace SoftSquare.AlAhlyClub.Application.Common.ExceptionHandlers;

public class UnauthorizedException : ServerException
{
    public UnauthorizedException(string message)
        : base(message, HttpStatusCode.Unauthorized)
    {
    }
}