namespace OpenModular.Common.Utils.Exceptions;

/// <summary>
/// OpenModular异常基类
/// </summary>
public class ExceptionBase : Exception
{
    public ExceptionBase()
    {

    }

    public ExceptionBase(string? message) : base(message)
    {

    }

    public ExceptionBase(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}