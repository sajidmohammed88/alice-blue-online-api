using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AliceBlueOnlineLibrary.DataContract.Enum;
using AliceBlueOnlineLibrary.Helper;
using Newtonsoft.Json;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class CompactData
    {
        public Exchange Exchange { get; set; }

        public uint Token { get; set; }

        public decimal Ltp { get; set; }

        public uint Change { get; set; }

        public uint ExchangeTimeStamp { get; set; }

        public uint Volume { get; set; }

        internal static CompactData Deserialize(byte[] data)
        {
            using (MemoryStream m = new MemoryStream(data))
            {
                using (BinaryReader reader = new BinaryReader(m))
                {
                    Exchange exchange = (Exchange)reader.ReadByte();
                    return new CompactData
                    {
                        Exchange = exchange,
                        Token = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        Ltp = decimal.Divide(Helper.Helper.ReverseBytes(reader.ReadUInt32()), exchange == Exchange.CDS ? 10000000 : 100),
                        Change = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        ExchangeTimeStamp = Helper.Helper.ReverseBytes(reader.ReadUInt32()),
                        Volume = Helper.Helper.ReverseBytes(reader.ReadUInt32())
                    };
                }
            }
        }
    }
}
