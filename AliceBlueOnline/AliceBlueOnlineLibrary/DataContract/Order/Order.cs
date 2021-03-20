using AliceBlueOnlineLibrary.DataContract.Order.Enum;
using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order
{
    public class Order : PlaceRequestBase
    {
        [JsonProperty("trigger_price")]
        public decimal TriggerPrice { get; set; }

        public ProductType Product { get; set; }
    }
}
