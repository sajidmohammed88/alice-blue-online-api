using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.CashPositions.Data
{
    public class AvailableMargins
    {
        [JsonProperty("pay_in")]
        public string PayIn { get; set; }

        public string NotionalCash { get; set; }

        [JsonProperty("direct_collateral_value")]
        public string DirectCollateralValue { get; set; }

        public string Credits { get; set; }

        [JsonProperty("collateral_value")]
        public string CollateralValue { get; set; }

        [JsonProperty("cashmarginavailable")]
        public string CashMarginAvailable { get; set; }

        [JsonProperty("adhoc_margin")]
        public string AdhocMargin { get; set; }
    }
}
