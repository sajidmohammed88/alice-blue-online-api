using System;
using System.Collections.Generic;
using System.Linq;
using AliceBlueOnlineLibrary.DataContract.Enum;

namespace AliceBlueOnlineLibrary.DataContract.Feeds
{
    public class FullSnapQuote
    {
        public Exchange Exchange { get; set; }

        public uint Token { get; set; }

        public IList<uint> Buyers { get; set; }

        public IList<decimal> BidPrices { get; set; }

        public IList<uint> BidQuantities { get; set; }

        public IList<uint> Sellers { get; set; }

        public IList<decimal> AskPrices { get; set; }

        public IList<uint> AskQuantities { get; set; }

        public decimal Atp { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public ulong TotalBuyQuantity { get; set; }

        public ulong TotalSellQuantity { get; set; }

        public uint Volume { get; set; }

        internal static FullSnapQuote Deserialize(byte[] data)
        {
            List<byte> dataList = data.ToList();
            Exchange exchange = (Exchange)dataList[0];
            int multiplier = exchange == Exchange.CDS ? 10000000 : 100;

            return new FullSnapQuote
            {
                Exchange = (Exchange)dataList[0],
                Token = Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(1, 4).ToArray(), 0)),
                Buyers = Helper.Helper.FillIntList(dataList, 5, 20),
                BidPrices = Helper.Helper.FillDecimalList(dataList, 25, 20, multiplier),
                BidQuantities = Helper.Helper.FillIntList(dataList, 45, 20),
                Sellers = Helper.Helper.FillIntList(dataList, 65, 20),
                AskPrices = Helper.Helper.FillDecimalList(dataList, 85, 20, multiplier),
                AskQuantities = Helper.Helper.FillIntList(dataList, 105, 20),
                Atp = decimal.Divide(Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(125, 4).ToArray(), 0)), multiplier),
                Open = decimal.Divide(Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(129, 4).ToArray(), 0)), multiplier),
                High = decimal.Divide(Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(133, 4).ToArray(), 0)), multiplier),
                Low = decimal.Divide(Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(137, 4).ToArray(), 0)), multiplier),
                Close = decimal.Divide(Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(141, 4).ToArray(), 0)), multiplier),
                TotalBuyQuantity = Helper.Helper.ReverseBytes(BitConverter.ToUInt64(dataList.GetRange(145, 8).ToArray(), 0)),
                TotalSellQuantity = Helper.Helper.ReverseBytes(BitConverter.ToUInt64(dataList.GetRange(153, 8).ToArray(), 0)),
                Volume = Helper.Helper.ReverseBytes(BitConverter.ToUInt32(dataList.GetRange(161, 4).ToArray(), 0))
            };
        }
    }
}
