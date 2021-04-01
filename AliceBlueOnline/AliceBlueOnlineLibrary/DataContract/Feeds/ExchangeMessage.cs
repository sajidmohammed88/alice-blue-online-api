using System;
using System.IO;
using System.Text;
using AliceBlueOnlineLibrary.DataContract.Enum;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class ExchangeMessage
    {
        public Exchange Exchange { get; set; }

        public ushort Length { get; set; }

        public string Message { get; set; }

        public uint ExchangeTimeStamp { get; set; }

        internal static ExchangeMessage Deserialize(byte[] data)
        {
            ushort length = Helper.Helper.ReverseBytes(BitConverter.ToUInt16(data, 1));
            return new ExchangeMessage
            {
                Exchange = (Exchange)data[0],
                Length = length,
                Message = Encoding.Default.GetString(data, 3, length),
                ExchangeTimeStamp = Helper.Helper.ReverseBytes(BitConverter.ToUInt32(data, length + 3))
            };
        }
    }
}
