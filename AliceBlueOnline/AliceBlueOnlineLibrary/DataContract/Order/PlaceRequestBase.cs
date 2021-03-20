using AliceBlueOnlineLibrary.DataContract.Enum;
using AliceBlueOnlineLibrary.DataContract.Order.Enum;
using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order
{
    public class PlaceRequestBase
    {
        public Exchange Exchange { get; set; }

        [JsonProperty("order_type")]
        public OrderType OrderType { get; set; }

        [JsonProperty("instrument_token")]
        public int InstrumentToken { get; set; }

        public int Quantity { get; set; }

        [JsonProperty("disclosed_quantity")]
        public int DisclosedQuantity { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("transaction_type")]
        public OrderSide TransactionType { get; set; }

        public string Validity { get; set; }
    }
}
