using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Request
{
    public class PlaceOrderOrAmoRequest : Order
    {
        public string Source { get; set; }

        [JsonProperty("order_tag")]
        public string OrderTag { get; set; }
    }
}
