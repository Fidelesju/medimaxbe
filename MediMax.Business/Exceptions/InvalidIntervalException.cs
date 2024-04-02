using System;

namespace MediMax.Business.Exceptions
{
    public class InvalidIntervalException : Exception
    {
        public InvalidIntervalException(string message) : base(message)
        {
        }

        public InvalidIntervalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
