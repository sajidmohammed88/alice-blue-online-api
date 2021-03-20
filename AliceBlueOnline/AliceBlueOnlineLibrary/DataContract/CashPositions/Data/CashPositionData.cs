using Newtonsoft.Json;
using System.Collections.Generic;

namespace AliceBlueOnlineLibrary.DataContract.CashPositions.Data
{
    public class CashPositionData
    {
        [JsonProperty("cash_positions")]
        public IList<CashPosition> CashPositions { get; set; }
    }
}
