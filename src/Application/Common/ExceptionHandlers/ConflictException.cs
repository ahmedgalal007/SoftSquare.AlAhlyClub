using System.Net;

namespace SoftSquare.AlAhlyClub.Application.Common.ExceptionHandlers;

public class ConflictException : ServerException
{
    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict)
    {
    }
}