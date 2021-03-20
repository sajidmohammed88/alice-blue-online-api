using AliceBlueOnlineLibrary.DataContract.Order.Response.Data;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response
{
    public class CancelOrderResponse : BaseMessage
    {
        public CancelOrderResponseData Data { get; set; }
    }
}
