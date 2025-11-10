namespace Lilys_CM.Application.Common.Exceptions;

public sealed class Lilys_CMConflictException : Exception
{
    public Lilys_CMConflictException(string message) : base(message) { }
}
