////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v1
{
  public class ExampleSetProvider : IExampleSetProvider
  {
    #region public members

    // Language {w1}
    public ExampleSet Lang01_ExampleSet
      => new Lang01_ExampleSets { Name = "01_L01_ES1" };

    // Language {x11z}
    public ExampleSet Lang02_ExampleSet
      => new Lang02_ExampleSets { Name = "02_L02_ES1" };

    // Several Accepting Paths
    public ExampleSet SAP_ExampleSet
      => new SAP_ExampleSets { Name = "03_SAP_ES1" };

    // Lot of Accepting Paths
    public ExampleSet LAP_ExampleSet
      => new LAP_ExampleSets { Name = "04_LAP_ES1" };

    // UP: Accepting Path May Not Exist
    public ExampleSet UPAPMNE_ExampleSet
      => new UPAPMNE_ExampleSets { Name = "05_UPAPMNE_ES1" };

    // UP: Accepting Path Always Exists;
    // NDMT has 2^n total amount of computation paths on inputs with length n;
    // on inputs with more than 32, NDTM requires >1 Gb of memory,
    // but MEAP takes 50 Mb of memory (if several TPL tasks used)
    public ExampleSet UPAPAE_ExampleSet
      => new UPAPAE_ExampleSets { Name = "06_UPAPAE_ES1" };

    // UP: Large Delta Relation (approx. 900 items in the example)
    public ExampleSet UPLDR_ExampleSet
      => new UPLDR_ExampleSets { Name = "07_UPLDR_ES1" };

    // Factoring Integers
    public ExampleSet IF_ExampleSetA
      => new IF_ExampleSetA { Name = "08_IF_ES1" };
    public ExampleSet IF_ExampleSetB
      => new IF_ExampleSetB { Name = "08_IF_ES2" };
    public ExampleSet IF_ExampleSetC
      => new IF_ExampleSetC { Name = "08_IF_ES3" };

    // Longest Common Subsequence
    public ExampleSet LCS_ExampleSet
      => new LCS_ExampleSet { Name = "09_LCS_ES1" };

    public IList<ExampleSet> ExampleSets { get; } = new List<ExampleSet>();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
