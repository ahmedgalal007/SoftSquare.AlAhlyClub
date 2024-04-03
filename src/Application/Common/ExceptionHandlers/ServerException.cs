using System.Net;

namespace SoftSquare.AlAhlyClub.Application.Common.ExceptionHandlers;

public class ServerException : Exception
{
    public ServerException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = new[] { message };
        StatusCode = statusCode;
    }

    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }
}