using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Request
{
    public class PlaceBracketOrderRequest : PlaceRequestBase
    {
        public string Source { get; set; }

        [JsonProperty("order_tag")]
        public string OrderTag { get; set; }

        [JsonProperty("square_off_value")]
        public decimal SquareOffValue { get; set; }

        [JsonProperty("stop_loss_value")]
        public decimal StopLossValue { get; set; }

        [JsonProperty("trailing_stop_loss")]
        public decimal TrailingStopLoss { get; set; }
    }
}
