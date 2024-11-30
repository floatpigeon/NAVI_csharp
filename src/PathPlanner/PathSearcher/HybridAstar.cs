using System.Collections.ObjectModel;
using System.Numerics;
using Maps;
using PathPlanner.Nodes;

namespace PathPlanner.HybridAstar;
class HybridAstar
{
    public HybridAstar(GridMap map) { Map = map; }
    public INode? PathSearch(Vector2 Begin, Vector2 End)
    {
        OpenList = new();
        Node BeginNode = new(Begin, null);
        Node.EndPosition = End;
        OpenList.Enqueue(BeginNode, BeginNode.TotalCost);

        while (OpenList.Count != 0)
        {
            INode CurrentNode = OpenList.Dequeue();
            Vector2 CurrendGrid = Map.PositionInWorldToGrid(CurrentNode.WorldPosition);

            if (Map.GetState(Map.PositionInWorldToGrid(CurrentNode.WorldPosition)) == State.CLOSED)
                continue;
            if (CurrentNode.WorldPosition == End)
                return CurrentNode;

            Map.StateChange(CurrendGrid, State.CLOSED);
            var Children = CurrentNode.SearchChildren;

            foreach (var Child in Children)
            {
                if (Accessible(Child))
                    OpenList.Enqueue(Child, Child.TotalCost);
            }
        }

        Console.WriteLine("Path Empty");
        return null;
    }
    private GridMap Map { get; set; }
    private PriorityQueue<INode, double> OpenList = new();
    private bool Accessible(INode ChildNode)
    {
        Vector2 ChildGrid = Map.PositionInWorldToGrid(ChildNode.WorldPosition);
        if (!Map.IsValid(ChildGrid))
            return false;
        if (Map.GetState(ChildGrid) == State.CLOSED || Map.GetState(ChildGrid) == State.OBSTACLE)
            return false;

        return true;
    }
}
