////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class DAGNode : IObjectWithId
  {
    #region Ctors

    public DAGNode(long id)
    {
      this.Id = id;

      this.InEdges = new List<DAGEdge>();
      this.OutEdges = new List<DAGEdge>();
    }

    #endregion

    #region public members

    public long Id { get; }

    public List<DAGEdge> InEdges { get; }
    public List<DAGEdge> OutEdges { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
