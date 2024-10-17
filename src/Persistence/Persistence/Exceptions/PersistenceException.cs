using OpenModular.Common.Utils.Exceptions;

namespace OpenModular.Persistence.Exceptions;

public class PersistenceException : ExceptionBase
{
    public PersistenceException()
    {

    }

    public PersistenceException(string message) : base(message)
    {

    }

    public PersistenceException(string message, Exception innerException) : base(message, innerException)
    {

    }
}