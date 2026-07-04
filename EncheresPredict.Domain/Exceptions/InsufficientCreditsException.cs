namespace EncheresPredict.Domain.Exceptions;

public class InsufficientCreditsException : Exception
{
    public InsufficientCreditsException()
        : base("The account does not have enough credits.")
    {
    }

    public InsufficientCreditsException(string message)
        : base(message)
    {
    }
}