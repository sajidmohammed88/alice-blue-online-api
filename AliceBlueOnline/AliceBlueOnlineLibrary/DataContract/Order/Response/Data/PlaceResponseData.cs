using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response.Data
{
    public class PlaceResponseData
    {
        [JsonProperty("oms_order_id")]
        public string OmsOrderId { get; set; }
    }
}