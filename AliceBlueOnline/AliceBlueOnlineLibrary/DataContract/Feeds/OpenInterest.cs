using System.IO;
using AliceBlueOnlineLibrary.DataContract.Enum;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class OpenInterest
    {
        public Exchange Exchange { get; set; }

        public uint Token { get; set; }

        public uint CurrentOpenInterest { get; set; }

        public uint InitialOpenInterest { get; set; }

        public uint ExchangeTimeStamp { get; set; }

        internal static OpenInterest Deserialize(byte[] data)
        {
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    Exchange exchange = (Exchange)reader.ReadByte();
                    int divideBy = exchange == Exchange.CDS ? 10000000 : 100;
                    return new OpenInterest
                    {
                        Exchange = (Exchange)reader.ReadByte(),
                        Token = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        CurrentOpenInterest = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        InitialOpenInterest = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        ExchangeTimeStamp = Helper.Helper.ReverseBytes(reader.ReadUInt32())
                    };
                }
            }
        }
    }
}
