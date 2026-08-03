namespace DevBooking.Application.Exceptions;

public sealed class ConflictException : BaseException
{
    public ConflictException(string message)
        : base(message)
    {
    }
}