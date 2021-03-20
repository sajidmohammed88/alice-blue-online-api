using AliceBlueOnlineLibrary.DataContract.Order.Response.Data;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response
{
    public class ModifyOrderResponse : BaseMessage
    {
        public ModifyOrderResponseData Data { get; set; }
    }
}
