////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;
using ExistsAcceptingPath;
using MTExtDefinitions.v2;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v2
{
  public class IF_CheckAlgorithm : ICheckAlgorithm
  {
    #region public members

    public bool CheckDecide(int[] input, bool result)
    {
      return true;
    }

    public bool CheckCompute(int[] input, int[] output)
    {
      byte[] inputBits = Core.AppHelper.CreateArrayCopy(input, e => (byte)e);
      BigInteger inputNumber = AppHelper.BitArrayToBigInteger(inputBits);

      int[] factorsByteString = Core.AppHelper.CreateArrayCopy<int, int>(output, t => t);

      (int[] factorXBits, int[] factorYBits) = IF_NDTM.RetrieveFactors(input.Length, factorsByteString);

      byte[] factorXBits_b = Core.AppHelper.CreateArrayCopy<int, byte>(factorXBits, t => (byte)t);
      byte[] factorYBits_b = Core.AppHelper.CreateArrayCopy<int, byte>(factorYBits, t => (byte)t);
      BigInteger factorXNumber = VerifyResults.AppHelper.BitArrayToBigInteger(factorXBits_b);
      BigInteger factorYNumber = VerifyResults.AppHelper.BitArrayToBigInteger(factorYBits_b);

      BigInteger xMultYNumber = factorXNumber * factorYNumber;
      byte[] xMultYBits = VerifyResults.AppHelper.BigIntegerToBitArray(xMultYNumber);

      bool result = (xMultYNumber == inputNumber);

      Ensure.That(result).IsTrue();

      log.InfoFormat(
        "{0} mult by {1} = {2}",
        factorXNumber.ToString(),
        factorYNumber.ToString(),
        xMultYNumber.ToString());

      return result;
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
