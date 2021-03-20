using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Holdings
{
    public class Holding
    {
        [JsonProperty("withheld_qty")]
        public string WithheldQty { get; set; }

        [JsonProperty("used_quantity")]
        public string UsedQuantity { get; set; }

        [JsonProperty("trading_symbol")]
        public string TradingSymbol { get; set; }

        [JsonProperty("target_product")]
        public string TargetProduct { get; set; }

        [JsonProperty("t1_quantity")]
        public string T1Quantity { get; set; }

        public string Quantity { get; set; }

        public string Product { get; set; }

        public string Price { get; set; }

        [JsonProperty("nse_ltp")]
        public string NseLtp { get; set; }

        public string Isin { get; set; }

        [JsonProperty("instrument_token")]
        public string InstrumentToken { get; set; }

        [JsonProperty("holding_update_quantity")]
        public string HoldingUpdateQuantity { get; set; }

        public string Haircut { get; set; }

        public string Exchange { get; set; }

        [JsonProperty("collateral_update_quantity")]
        public string CollateralUpdateQuantity { get; set; }

        [JsonProperty("collateral_type")]
        public string CollateralType { get; set; }

        [JsonProperty("collateral_quantity")]
        public string CollateralQuantity { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("buy_avg")]
        public decimal BuyAvg { get; set; }

        [JsonProperty("bse_ltp")]
        public string BseLtp { get; set; }
    }
}
