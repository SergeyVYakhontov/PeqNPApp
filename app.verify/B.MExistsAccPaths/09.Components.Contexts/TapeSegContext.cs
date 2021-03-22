////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class TapeSegContext
  {
    #region Ctors

    public TapeSegContext()
    {
      TapeSegTConsistPath = new List<long>();
      TapeSegOutput = Array.Empty<int>();
    }

    public TapeSegContext(TapeSegContext tapeSegContext)
    {
      this.TapeSeg = new LongSegment(tapeSegContext.TapeSeg);
      this.PartialTConsistPath = new List<long>(tapeSegContext.PartialTConsistPath);
      this.TConsistPathNodes = new List<long>(tapeSegContext.TConsistPathNodes);
    }

    #endregion

    #region public members

    public LongSegment TapeSeg { get; set; } = default!;

    // the result
    public bool TapeSegPathExists { get; set; }
    public bool TapeSegPathFound { get; set; }
    public List<long> TapeSegTConsistPath { get; set; } = new();
    public int[] TapeSegOutput { get; set; } = Array.Empty<int>();

    // tape seg commodities
    public SortedDictionary<long, Commodity> KSetCommodities { get; set; } = new();
    public SortedDictionary<long, SortedSet<long>> KSetZetaSubset { get; set; } = new();

    // unused nodes found during optimization steps
    public SortedSet<long> TArbSeqCFGUnusedNodes { get; set; } = new();

    // finding linear equations solution
    public SortedDictionary<long, SortedDictionary<long, SortedSet<long>>> KiToZetaToKjIntSet { get; set; } = new();
    public SortedDictionary<long, SortedDictionary<long, SortedSet<long>>> KjToZetaToKiIntSet { get; set; } = new();
    public SortedSet<KeyValuePair<long, long>> StrongConnCommodityPairs { get; set; } = new();
    public string MathQueryString { get; set; } = string.Empty;
    public Single[] TCPELinProgSolution { get; set; } = Array.Empty<Single>();

    // finding path
    public List<long> PartialTConsistPath { get; set; } = new();
    public List<long> TConsistPathNodes { get; set; } = new();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
