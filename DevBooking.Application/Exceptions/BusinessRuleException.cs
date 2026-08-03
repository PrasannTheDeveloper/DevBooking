namespace DevBooking.Application.Exceptions;

public sealed class BusinessRuleException : BaseException
{
    public BusinessRuleException(string message)
        : base(message)
    {
    }
}
