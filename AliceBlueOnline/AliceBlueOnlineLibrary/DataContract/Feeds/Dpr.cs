using System.IO;
using AliceBlueOnlineLibrary.DataContract.Enum;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class Dpr
    {
        public Exchange Exchange { get; set; }

        public uint Token { get; set; }

        public uint ExchangeTimeStamp { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        internal static Dpr Deserialize(byte[] data)
        {
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    Exchange exchange = (Exchange)reader.ReadByte();
                    int divideBy = exchange == Exchange.CDS ? 10000000 : 100;
                    return new Dpr
                    {
                        Exchange = exchange,
                        Token = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        ExchangeTimeStamp = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        HighPrice = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy),
                        LowPrice = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), divideBy)
                    };
                }
            }
        }
    }
}
