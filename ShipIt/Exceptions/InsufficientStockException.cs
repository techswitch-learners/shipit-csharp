namespace ShipIt.Exceptions
{
    public class InsufficientStockException: ClientVisibleException
    {
        public InsufficientStockException(string message) : base(message, ErrorCode.INSUFFICIENT_STOCK)
        {
        }
    }
}