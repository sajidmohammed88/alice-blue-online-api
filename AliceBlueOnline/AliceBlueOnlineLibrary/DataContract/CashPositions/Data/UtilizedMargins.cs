using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.CashPositions.Data
{
    public class UtilizedMargins
    {
        [JsonProperty("var_margin")]
        public string VarMargin { get; set; }

        [JsonProperty("unrealised_m2m")]
        public string UnrealizedM2M { get; set; }

        [JsonProperty("span_margin")]
        public string SpanMargin { get; set; }

        [JsonProperty("realised_m2m")]
        public string RealizedM2M { get; set; }

        [JsonProperty("premium_present")]
        public string PremiumPresent { get; set; }

        [JsonProperty("pay_out")]
        public string PayOut { get; set; }

        public string Multiplier { get; set; }

        [JsonProperty("exposure_margin")]
        public string ExposureMargin { get; set; }

        public string Elm { get; set; }

        public string Debits { get; set; }
    }
}
