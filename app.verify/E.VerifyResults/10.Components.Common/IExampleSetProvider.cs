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
  public interface IExampleSetProvider
  {
    #region public members

    // Language {w1}
    ExampleSet Lang01_ExampleSet { get; }

    // Language {x11z}
    ExampleSet Lang02_ExampleSet { get; }

    // Several Accepting Paths
    ExampleSet SAP_ExampleSet { get; }

    // Lot of Accepting Paths
    ExampleSet LAP_ExampleSet { get; }

    // UP: Accepting Path May Not Exist
    ExampleSet UPAPMNE_ExampleSet { get; }

    // UP: Accepting Path Always Exists;
    // NDMT has 2^n total amount of computation paths on inputs with length n;
    // on inputs with more than 32, NDTM requires >1 Gb of memory,
    // but MEAP takes 50 Mb of memory (if several TPL tasks used)
    ExampleSet UPAPAE_ExampleSet { get; }

    // UP: Large Delta Relation (approx. 900 items in the example)
    ExampleSet UPLDR_ExampleSet { get; }

    // Factoring Integers
    ExampleSet IF_ExampleSetA { get; }

    // Longest Common Subsequence
    ExampleSet LCS_ExampleSet { get; }

    IList<ExampleSet> ExampleSets { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
