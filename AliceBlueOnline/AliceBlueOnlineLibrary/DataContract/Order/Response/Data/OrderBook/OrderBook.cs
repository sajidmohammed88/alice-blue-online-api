using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response.Data.OrderBook
{
    public class OrderBook : Order
    {
        [JsonProperty("user_order_id")]
        public string UserOrderId { get; set; }

        [JsonProperty("trading_symbol")]
        public string TradingSymbol { get; set; }

        [JsonProperty("remaining_quantity")]
        public int RemainingQuantity { get; set; }

        [JsonProperty("rejection_reason")]
        public string RejectionReason { get; set; }

        [JsonProperty("order_tag")]
        public string OrderTag { get; set; }

        [JsonProperty("order_status")]
        public string OrderStatus { get; set; }

        [JsonProperty("order_entry_time")]
        public int OrderEntryTime { get; set; }

        [JsonProperty("oms_order_id")]
        public string OmsOrderId { get; set; }

        [JsonProperty("nest_request_id")]
        public string NestRequestId { get; set; }

        [JsonProperty("lotsize")]
        public int LotSize { get; set; }

        [JsonProperty("login_id")]
        public string LoginId { get; set; }

        [JsonProperty("leg_order_indicator")]
        public string LegOrderIndicator { get; set; }

        [JsonProperty("filled_quantity")]
        public int FilledQuantity { get; set; }

        [JsonProperty("exchange_time")]
        public int ExchangeTime { get; set; }

        [JsonProperty("exchange_order_id")]
        public string ExchangeOrderId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("average_price")]
        public decimal AveragePrice { get; set; }
    }
}
