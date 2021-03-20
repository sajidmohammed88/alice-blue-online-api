using AliceBlueOnlineLibrary.DataContract.Order.Response.Data;
using System.Collections.Generic;

namespace AliceBlueOnlineLibrary.DataContract.Order.Response
{
    public class OrderHistoryResponse : BaseMessage
    {
        public IList<OrderHistoryResponseData> Data { get; set; }
    }
}
