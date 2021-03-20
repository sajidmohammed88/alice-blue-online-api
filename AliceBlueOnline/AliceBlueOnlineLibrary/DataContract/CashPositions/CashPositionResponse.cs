using AliceBlueOnlineLibrary.DataContract.CashPositions.Data;

namespace AliceBlueOnlineLibrary.DataContract.CashPositions
{
    public class CashPositionResponse : BaseMessage
    {
        public CashPositionData Data { get; set; }
    }
}
