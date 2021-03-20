using Newtonsoft.Json;
using System.Collections.Generic;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response.Data.OrderBook
{
    public class OrderBookResponseData
    {
        [JsonProperty("pending_orders")]
        public IList<OrderBook> PendingOrders { get; set; }

        [JsonProperty("completed_orders")]
        public IList<OrderBook> CompletedOrders { get; set; }
    }
}
