using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public interface IBitVectorProvider
  {
    IBitVector CreateVector(ulong size);
    IBitVector CreateVector(IBitVector v);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
