using System;
using System.Collections.Generic;
using System.Linq;

namespace AliceBlueOnlineLibrary.Helper
{
    internal static class Helper
    {
        public static ushort ReverseBytes(ushort value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value;
            }

            return (ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        public static uint ReverseBytes(uint value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value;
            }

            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        public static ulong ReverseBytes(ulong value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value;
            }

            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

        public static IEnumerable<List<T>> SplitList<T>(this List<T> bigList, int nSize = 4)
        {
            for (int i = 0; i < bigList.Count; i += nSize)
            {
                yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
            }
        }

        public static IList<uint> FillIntList(List<byte> data, int start, int count)
        {
            return data
                .GetRange(start, count)
                .SplitList()
                .Select(ui => ReverseBytes(BitConverter.ToUInt32(ui.ToArray(), 0)))
                .ToList();
        }

        public static IList<decimal> FillDecimalList(List<byte> data, int start, int count, int multiplier = 100)
        {
            return data
                .GetRange(start, count)
                .SplitList()
                .Select(ui => decimal.Divide(ReverseBytes(BitConverter.ToUInt32(ui.ToArray(), 0)), multiplier))
                .ToList();
        }
    }
}
