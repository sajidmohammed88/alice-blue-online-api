using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Trade
{
    public class Trade
    {
        [JsonProperty("user_order_id")]
        public string UserOrderId { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("trading_symbol")]
        public string TradingSymbol { get; set; }

        [JsonProperty("trade_price")]
        public decimal TradePrice { get; set; }

        [JsonProperty("trade_id")]
        public string TradeId { get; set; }

        public string Product { get; set; }

        [JsonProperty("order_entry_time")]
        public int OrderEntryTime { get; set; }

        [JsonProperty("oms_order_id")]
        public string OmsOrderId { get; set; }

        [JsonProperty("instrument_token")]
        public string InstrumentToken { get; set; }

        [JsonProperty("filled_quantity")]
        public int FilledQuantity { get; set; }

        [JsonProperty("exchange_time")]
        public int ExchangeTime { get; set; }

        [JsonProperty("exchange_order_id")]
        public string ExchangeOrderId { get; set; }

        public string Exchange { get; set; }
    }
}
