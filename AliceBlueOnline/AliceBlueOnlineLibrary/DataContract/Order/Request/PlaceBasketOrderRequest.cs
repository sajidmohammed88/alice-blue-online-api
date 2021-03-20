using System.Collections.Generic;

namespace AliceBlueOnlineLibrary.DataContract.Order.Request
{
    public class PlaceBasketOrderRequest
    {
        public string Source { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
