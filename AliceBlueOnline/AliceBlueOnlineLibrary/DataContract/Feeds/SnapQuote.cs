using System;
using System.Collections.Generic;
using System.Linq;
using AliceBlueOnlineLibrary.DataContract.Enum;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class SnapQuote
    {
        public Exchange Exchange { get; set; }

        public uint Token { get; set; }

        public IList<uint> Buyers { get; set; }

        public IList<decimal> BidPrices { get; set; }

        public IList<uint> BidQuantities { get; set; }

        public IList<uint> Sellers { get; set; }

        public IList<decimal> AskPrices { get; set; }

        public IList<uint> AskQuantities { get; set; }

        public uint ExchangeTimeStamp { get; set; }

        internal static SnapQuote Deserialize(byte[] data)
        {
            List<byte> dataList = data.ToList();
            Exchange exchange = (Exchange)dataList[0];
            int multiplier = exchange == Exchange.CDS ? 10000000 : 100;

            return new SnapQuote
            {
                Exchange = exchange,
                Token = Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(1, 4).ToArray(), 0)),
                Buyers = Helper.Helper.FillIntList(dataList, 5, 20),
                BidPrices = Helper.Helper.FillDecimalList(dataList, 25, 20, multiplier),
                BidQuantities = Helper.Helper.FillIntList(dataList, 45, 20),
                Sellers = Helper.Helper.FillIntList(dataList, 65, 20),
                AskPrices = Helper.Helper.FillDecimalList(dataList, 85, 20, multiplier),
                AskQuantities = Helper.Helper.FillIntList(dataList, 105, 20),
                ExchangeTimeStamp = Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(125, 4).ToArray(), 0))
            };
        }
    }
}
