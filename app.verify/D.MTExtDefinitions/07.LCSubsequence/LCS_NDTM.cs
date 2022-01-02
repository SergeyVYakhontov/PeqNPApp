////////////////////////////////////////////////////////////////////////////////////////////////////
// LCS_NDTM: Turing machine for finding longest common subsequense (to implement)
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions
{
  // TODO: implement
  public class LCS_NDTM : OneTapeNDTM
  {
    #region Ctors

    public LCS_NDTM()
      : base("NDTM") {}

    #endregion

    #region public members

    public override void Setup()
    {
      CheckDeltaRelation();
    }

    public override bool UP => false;
    public override bool AcceptingPathAlwaysExists => true;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
