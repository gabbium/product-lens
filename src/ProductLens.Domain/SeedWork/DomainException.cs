namespace ProductLens.Domain.SeedWork;

public class DomainException(Error error) : Exception(error.Code)
{
    public Error Error { get; } = error;
}
