namespace SoftSquare.AlAhlyClub.Application.Common.ExceptionHandlers;

public class InternalServerException : ServerException
{
    public InternalServerException(string message)
        : base(message)
    {
    }
}