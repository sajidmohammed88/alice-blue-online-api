using AliceBlueOnlineLibrary.DataContract.Order.Response.Data.OrderBook;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response
{
    public class OrderBookResponse : BaseMessage
    {
        public OrderBookResponseData Data { get; set; }
    }
}
