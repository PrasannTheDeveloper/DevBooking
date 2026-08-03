namespace DevBooking.Application.Exceptions;

public sealed class ForbiddenException : BaseException
{
    public ForbiddenException(string message)
        : base(message)
    {
    }
}