using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response.Data
{
    public class OrderHistoryResponseData
    {
        public string Validity { get; set; }

        [JsonProperty("trigger_price")]
        public decimal TriggerPrice { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("trading_symbol")]
        public string TradingSymbol { get; set; }

        [JsonProperty("quantity_to_fill")]
        public int QuantityToFill { get; set; }

        public string Product { get; set; }

        [JsonProperty("price_to_fill")]
        public decimal PriceToFill { get; set; }

        [JsonProperty("order_status")]
        public string OrderStatus { get; set; }

        [JsonProperty("oms_order_id")]
        public string OmsOrderId { get; set; }

        [JsonProperty("nest_request_id")]
        public string NestRequestId { get; set; }

        [JsonProperty("exchange_time")]
        public string ExchangeTime { get; set; }

        [JsonProperty("exchange_order_id")]
        public string ExchangeOrderId { get; set; }

        public string Exchange { get; set; }

        [JsonProperty("disclosed_quantity")]
        public string DisclosedQuantity { get; set; }

        [JsonProperty("average_price")]
        public decimal AveragePrice { get; set; }
    }
}
