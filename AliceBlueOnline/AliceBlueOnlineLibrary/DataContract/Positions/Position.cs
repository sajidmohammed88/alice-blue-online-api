using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Positions
{
    public class Position
    {
        [JsonProperty("unrealised_pnl")]
        public string UnrealizedPnl { get; set; }

        [JsonProperty("trading_symbol")]
        public string TradingSymbol { get; set; }

        [JsonProperty("total_sell_quantity")]
        public int TotalSellQuantity { get; set; }

        [JsonProperty("total_buy_quantity")]
        public int TotalBuyQuantity { get; set; }

        [JsonProperty("strike_price")]
        public string StrikePrice { get; set; }

        [JsonProperty("sell_quantity")]
        public string SellQuantity { get; set; }

        [JsonProperty("sell_amount")]
        public string SellAmount { get; set; }

        [JsonProperty("realised_pnl")]
        public string RealizedPnl { get; set; }

        public string Product { get; set; }

        [JsonProperty("oms_order_id")]
        public string OmsOrderId { get; set; }

        [JsonProperty("net_quantity")]
        public int NetQuantity { get; set; }

        [JsonProperty("net_amount")]
        public decimal NetAmount { get; set; }

        public int Multiplier { get; set; }

        public string M2M { get; set; }

        public decimal Ltp { get; set; }

        [JsonProperty("instrument_token")]
        public string InstrumentToken { get; set; }

        [JsonProperty("fill_id")]
        public string FillId { get; set; }

        public string Exchange { get; set; }

        public bool Enabled { get; set; }

        [JsonProperty("close_price")]
        public decimal ClosePrice { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("cf_sell_quantity")]
        public int CfSellQuantity { get; set; }

        [JsonProperty("cf_buy_quantity")]
        public int CfBuyQuantity { get; set; }

        [JsonProperty("cf_average_sell_price")]
        public decimal CfAverageSellPrice { get; set; }

        [JsonProperty("cf_average_buy_price")]
        public decimal CfAverageBuyPrice { get; set; }

        [JsonProperty("buy_quantity")]
        public string BuyQuantity { get; set; }

        [JsonProperty("buy_amount")]
        public string BuyAmount { get; set; }

        public string Bep { get; set; }

        [JsonProperty("average_sell_price")]
        public string AverageSellPrice { get; set; }

        [JsonProperty("average_buy_price")]
        public string AverageBuyPrice { get; set; }
    }
}
