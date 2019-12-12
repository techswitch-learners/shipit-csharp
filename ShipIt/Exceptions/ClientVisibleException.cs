using System;

namespace ShipIt.Exceptions
{
    public abstract class ClientVisibleException : Exception
    {
        public ErrorCode ErrorCode { get; }

        protected ClientVisibleException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}