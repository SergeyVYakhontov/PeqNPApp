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
  public class LinEquationContext
  {
    #region public members

    public LinEquationsMatrix TCPELinProgMatrix { get; set; } = default!;
    public DAGLinEquationsSet TCPELinProgEqsSet { get; set; } = default!;
    public DAGLinEquationsSet TArbSeqCFGLinProgEqsSet { get; set; } = default!;
    public SortedDictionary<long, DAGLinEquationsSet> KiLinProgEqsSets { get; set; } = new();
    public SortedDictionary<long, DAGLinEquationsSet> KSetZetaLinProgEqsSets { get; set; } = new();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
