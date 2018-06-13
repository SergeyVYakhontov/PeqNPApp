////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public static partial class AppHelper
  {
    #region public members

    public static BigInteger BitArrayToBigInteger(byte[] bitArray)
    {
      byte[] numberBytes = new byte[bitArray.Length / 8 + 1];

      for(long i = 0; i < bitArray.Length; i++)
      {
        byte b = bitArray[i];

        if(b == 1)
        {
          numberBytes[i / 8] |= (byte)(1 << (int)(i % 8));
        }
      }

      return new BigInteger(numberBytes);
    }

    public static byte[] BigIntegerToBitArray(BigInteger bigInteger)
    {
      byte[] byteArray = bigInteger.ToByteArray();
      List<byte> bitList = new List<byte>();

      for (long i = 0; i < byteArray.Length; i++)
      {
        byte b = byteArray[i];

        for (int j = 0; j < 8; j++)
        {
          byte t = (byte)(b & (1 << j));
          bitList.Add((byte)(t == 0 ? 0 : 1));
        }
      }

      int lastBit1 = bitList.FindLastIndex(e => (e == 1));
      bitList = bitList.GetRange(0, lastBit1 + 1);

      bitList.Reverse();

      return bitList.ToArray();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
