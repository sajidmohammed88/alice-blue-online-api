using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response.Data
{
    public class ModifyOrderResponseData
    {
        [JsonProperty("oms_order_id")]
        public string[] OmsOrderIds { get; set; }
    }
}