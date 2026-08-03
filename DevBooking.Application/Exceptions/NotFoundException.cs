namespace DevBooking.Application.Exceptions;

public sealed class NotFoundException : BaseException
{
    public NotFoundException(string message)
        : base(message)
    {
    }
}