namespace DevBooking.Application.Exceptions;

public sealed class ValidationException : BaseException
{
    public ValidationException(string message)
        : base(message)
    {
    }
}