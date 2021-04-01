using AliceBlueOnlineLibrary.DataContract.Enum;
using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Contracts
{
    public class Instrument
    {
        [JsonProperty("trading_symbol")]
        public string TradingSymbol { get; set; }

        public string Symbol { get; set; }

        public string LotSize { get; set; }

        [JsonProperty("exchange_code")]
        public int ExchangeCode { get; set; }

        public Exchange Exchange { get; set; }

        public string Company { get; set; }

        [JsonProperty("code")]
        public int Token { get; set; }
    }
}
