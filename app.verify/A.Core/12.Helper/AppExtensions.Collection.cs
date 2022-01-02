////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static class AppExtensions
  {
    #region public members

    public static long LastIndex<T>(this T[] data)
    {
      return data.Length - 1;
    }
  }

  #endregion
}
