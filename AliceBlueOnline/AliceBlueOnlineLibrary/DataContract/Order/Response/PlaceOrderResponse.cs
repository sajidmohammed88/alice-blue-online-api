using AliceBlueOnlineLibrary.DataContract.Order.Response.Data;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response
{
    public class PlaceOrderResponse : BaseMessage
    {
        public PlaceResponseData Data { get; set; }
    }
}
