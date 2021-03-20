namespace AliceBlueOnlineLibrary.DataContract.CashPositions.Data
{
    public class CashPosition
    {
        public UtilizedMargins Utilized { get; set; }

        public string Segment { get; set; }

        public string Net { get; set; }

        public string Category { get; set; }

        public AvailableMargins Available { get; set; }
    }
}
