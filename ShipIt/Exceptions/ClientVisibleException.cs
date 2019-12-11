using System;

namespace ShipIt.Exceptions
{
    public enum ErrorCode
    {
        NO_SUCH_ENTITY_EXCEPTION,
        MALFORMED_REQUEST,
        INVALID_STATE,
        INSUFFICIENT_STOCK
    }

    public abstract class ClientVisibleException : Exception
    {
        public ErrorCode ErrorCode { get; }

        protected ClientVisibleException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }

    public class InsufficientStockException: ClientVisibleException
    {
        public InsufficientStockException(string message) : base(message, ErrorCode.INSUFFICIENT_STOCK)
        {
        }
    }

    public class InvalidStateException : ClientVisibleException
    {
        public InvalidStateException(string message) : base(message, ErrorCode.INVALID_STATE)
        {
        }
    }

    public class MalformedRequestException : ClientVisibleException
    {
        public MalformedRequestException(string message) : base(message, ErrorCode.MALFORMED_REQUEST)
        {
        }
    }

    public class NoSuchEntityException : ClientVisibleException
    {
        public NoSuchEntityException(string message) : base(message, ErrorCode.NO_SUCH_ENTITY_EXCEPTION)
        {
        }
    }

    public class ValidationException : MalformedRequestException
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}