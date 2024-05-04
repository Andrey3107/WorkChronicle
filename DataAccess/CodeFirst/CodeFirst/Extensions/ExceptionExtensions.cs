namespace CodeFirst.Extensions
{
    using System;

    public static class ExceptionExtensions
    {
        public static Exception GetLastException(this Exception exception)
        {
            var innerException = exception.InnerException;
            return innerException != null ? innerException.GetLastException() : exception;
        }
    }
}
