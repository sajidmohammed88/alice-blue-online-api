using System.IO;
using AliceBlueOnlineLibrary.DataContract.Enum;
using AliceBlueOnlineLibrary.Helper;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class MarketData
    {
        public Exchange Exchange { get; set; }

        public uint Token { get; set; }

        public decimal Ltp { get; set; }

        public uint Ltt { get; set; }

        public uint Ltq { get; set; }

        public uint Volume { get; set; }

        public decimal BestBidPrice { get; set; }

        public uint BestBidQuantity { get; set; }

        public decimal BestAskPrice { get; set; }

        public uint BestAskQuantity { get; set; }

        public ulong TotalBuyQuantity { get; set; }

        public ulong TotalSellQuantity { get; set; }

        public decimal Atp { get; set; }

        public uint ExchangeTimeStamp { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal YearlyHigh { get; set; }

        public decimal YearlyLow { get; set; }

        internal static MarketData Deserialize(byte[] data)
        {
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    Exchange exchange = (Exchange)reader.ReadByte();
                    int divideBy = exchange == Exchange.CDS ? 10000000 : 100;
                    return new MarketData
                    {
                        Exchange = exchange,
                        Token = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        Ltp = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        Ltt = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        Ltq = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        Volume = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        BestBidPrice = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        BestBidQuantity = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        BestAskPrice = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        BestAskQuantity = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        TotalBuyQuantity = Helper.Helper.ReverseBytes(reader.ReadUInt64()),
                        TotalSellQuantity = Helper.Helper.ReverseBytes(reader.ReadUInt64()),
                        Atp = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        ExchangeTimeStamp = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        Open = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        High = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        Low = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        Close = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        YearlyHigh = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        YearlyLow = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy)
                    };
                }
            }
        }
    }
}
