using AliceBlueOnlineLibrary.DataContract.Order.Enum;
using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order
{
    public class Order : PlaceRequestBase
    {
        [JsonProperty("trigger_price")]
        public float TriggerPrice { get; set; }

        public ProductType Product { get; set; }
    }
}
