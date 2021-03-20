using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Request
{
    public class ModifyOrderRequest : Order
    {
        [JsonProperty("oms_order_id")]
        public string OmsOrderId { get; set; }
    }
}
