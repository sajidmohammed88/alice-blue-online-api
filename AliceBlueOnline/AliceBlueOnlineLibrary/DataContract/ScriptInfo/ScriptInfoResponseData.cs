using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.ScriptInfo
{
    public class ScriptInfoResponseData
    {
        [JsonProperty("permitted_to_trade")]
        public string PermittedToTrade { get; set; }

        public decimal Warning { get; set; }

        [JsonProperty("price_quotation")]
        public string PriceQuotation { get; set; }

        public string Series { get; set; }

        [JsonProperty("price_units")]
        public string PriceUnits { get; set; }

        [JsonProperty("list_date")]
        public string ListDate { get; set; }

        public string Symbol { get; set; }

        [JsonProperty("circuit_rating")]
        public string CircuitRating { get; set; }

        public string Exchange { get; set; }

        [JsonProperty("im_spread_benifit")]
        public string ImSpreadBenefit { get; set; }

        [JsonProperty("last_trading_date")]
        public string LastTradingDate { get; set; }

        public string Remarks { get; set; }

        [JsonProperty("expulsion_date")]
        public string ExpulsionDate { get; set; }

        [JsonProperty("record_date")]
        public string RecordDate { get; set; }

        [JsonProperty("tick_size")]
        public decimal TickSize { get; set; }

        [JsonProperty("general_numerator")]
        public string GeneralNumerator { get; set; }

        [JsonProperty("board_lot_quantity")]
        public string BoardLotQuantity { get; set; }

        [JsonProperty("open_interest")]
        public int OpenInterest { get; set; }

        public string Dpr { get; set; }

        [JsonProperty("buy_var_margin")]
        public string BuyVarMargin { get; set; }

        [JsonProperty("expiry_date")]
        public string ExpiryDate { get; set; }

        [JsonProperty("issue_rate")]
        public int IssueRate { get; set; }

        [JsonProperty("delivery_units")]
        public string DeliveryUnits { get; set; }

        [JsonProperty("readmdate")]
        public string ReadMDate { get; set; }

        [JsonProperty("lower_circuit_limit")]
        public decimal LowerCircuitLimit { get; set; }

        public decimal Freeze { get; set; }

        [JsonProperty("price_numerator")]
        public string PriceNumerator { get; set; }

        [JsonProperty("max_order_size")]
        public string MaxOrderSize { get; set; }

        [JsonProperty("higher_circuit_limit")]
        public double HigherCircuitLimit { get; set; }

        [JsonProperty("tender_period_start_date")]
        public string TenderPeriodStartDate { get; set; }

        [JsonProperty("tender_period_end_date")]
        public string TenderPeriodEndDate { get; set; }

        [JsonProperty("market_type")]
        public string MarketType { get; set; }

        [JsonProperty("issue_start_date")]
        public string IssueStartDate { get; set; }

        [JsonProperty("issue_maturity_date")]
        public string IssueMaturityDate { get; set; }

        [JsonProperty("book_cls_start_time")]
        public string BookClsStartTime { get; set; }

        [JsonProperty("general_denominator")]
        public string GeneralDenominator { get; set; }

        [JsonProperty("delivery_start_date")]
        public string DeliveryStartDate { get; set; }

        [JsonProperty("instrument_type")]
        public string InstrumentType { get; set; }

        [JsonProperty("int_pay_date")]
        public string IntPayDate { get; set; }

        [JsonProperty("issue_capital")]
        public string IssueCapital { get; set; }

        [JsonProperty("delivery_end_date")]
        public string DeliveryEndDate { get; set; }

        [JsonProperty("price_denominator")]
        public string PriceDenominator { get; set; }

        [JsonProperty("sell_var_margin")]
        public string SellVarMargin { get; set; }

        [JsonProperty("other_buy_margin")]
        public string OtherBuyMargin { get; set; }

        [JsonProperty("other_sell_margin")]
        public string OtherSellMargin { get; set; }

        public string Comments { get; set; }

        [JsonProperty("nodel_start_date")]
        public string NodelStartDate { get; set; }

        [JsonProperty("quantity_units")]
        public string QuantityUnits { get; set; }

        [JsonProperty("exposure_margin")]
        public string ExposureMargin { get; set; }

        [JsonProperty("nodel_end_time")]
        public string NodelEndTime { get; set; }

        [JsonProperty("local_update_time")]
        public string LocalUpdateTime { get; set; }
    }
}
