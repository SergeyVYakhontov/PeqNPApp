////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static partial class AppHelper
  {
    #region public members

    public static long GCD(long a, long b)
    {
      if (a < b)
      {
        return GCD(b, a);
      }

      if (b == 1)
      {
        return 1;
      }

      long d = a / b;
      long c = a % b;

      if (c == 0)
      {
        return b;
      }

      return GCD(d, c);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
