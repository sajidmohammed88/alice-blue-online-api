using System;
using System.IO;
using System.Text;
using AliceBlueOnlineLibrary.DataContract.Enum;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class MarketStatus
    {
        public Exchange Exchange { get; set; }

        public ushort LengthOfMarketType { get; set; }

        public string MarketType { get; set; }

        public ushort LengthOfStatus { get; set; }

        public string Status { get; set; }

        internal static MarketStatus Deserialize(byte[] data)
        {
            ushort marketTypeLength = Helper.Helper.ReverseBytes(BitConverter.ToUInt16(data, 1));
            ushort statusLength = Helper.Helper.ReverseBytes(BitConverter.ToUInt16(data, marketTypeLength + 3));

            return new MarketStatus
            {
                Exchange = (Exchange)data[0],
                LengthOfMarketType = marketTypeLength,
                MarketType = Encoding.Default.GetString(data, 3, marketTypeLength),
                LengthOfStatus = statusLength,
                Status = Encoding.Default.GetString(data, marketTypeLength + 5, statusLength)
            };
        }
    }
}
