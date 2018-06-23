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
    #region public members

    public LongSegment TapeSeg { get; set; }

    // the result
    public bool TapeSegPathExists { get; set; }
    public bool TapeSegPathFound { get; set; }
    public List<long> TapeSegTConsistPath { get; set; }
    public int[] TapeSegOutput { get; set; }

    // tape seg commodities
    public SortedDictionary<long, Commodity> KSetCommodities { get; set; }
    public SortedDictionary<long, SortedSet<long>> KSetZetaSubset { get; set; }

    // unused nodes found during optimization steps
    public SortedSet<long> TArbSeqCFGUnusedNodes { get; set; }

    // finding linear equations solution
    public SortedDictionary<long, SortedDictionary<long, SortedSet<long>>> KiToZetaToKjIntSet { get; set; }
    public SortedDictionary<long, SortedDictionary<long, SortedSet<long>>> KjToZetaToKiIntSet { get; set; }
    public SortedSet<KeyValuePair<long, long>> StrongConnCommodityPairs { get; set; }
    public String MathQueryString { get; set; }
    public Single[] TCPELinProgSolution { get; set; }

    // finding path
    public List<long> PartialTConsistPath { get; set; }
    public List<long> TConsistPathNodes { get; set; }

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
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
