using System.Collections.ObjectModel;
using System.Numerics;
using Maps;
using PathPlanner.Nodes;

namespace PathPlanner.HybridAstar;
class HybridAstar
{
    public GridMap Map { get; private set; }
    private PriorityQueue<INode, double> OpenList = new();
    public HybridAstar(GridMap map) { Map = map; }

    public Vector2 PositionInWorldToGrid(Vector2 worldWorldPosition, float size)
    {
        int x = (int)(worldWorldPosition.X / size + 0.5);
        int y = (int)(worldWorldPosition.Y / size + 0.5);
        return new Vector2(x, y);
    }

    public INode? PathSearch(Vector2 Begin, Vector2 End)
    {
        Node BeginNode = new Node(Begin, null);
        OpenList.Enqueue(BeginNode, BeginNode.TotalCost);

        while (OpenList.Count != 0)
        {
            INode CurrentNode = OpenList.Dequeue();
            Vector2 CurrentGrid = PositionInWorldToGrid(CurrentNode.WorldPosition, 1);

            if (Map.GetState(CurrentGrid) == State.CLOSED)
                continue;

            if (CurrentNode.WorldPosition == End)
                return CurrentNode;

            Map.StateChange(CurrentNode.WorldPosition, State.CLOSED);
            Collection<INode> Children = CurrentNode.SearchChildren;

            for (int i = 0; i < Children.Count; i++)
            {
                if (Accessible(Children[i]))
                    OpenList.Enqueue(Children[i], Children[i].TotalCost);
            }
        }
        Console.WriteLine("OpenList Empty");
        return null;
    }

    private bool Accessible(INode ChildNode)
    {
        Vector2 ChildGrid = PositionInWorldToGrid(ChildNode.WorldPosition, 1);

        if (!Map.IsValid(ChildGrid))
            return false;
        if (Map.GetState(ChildGrid) == State.CLOSED || Map.GetState(ChildGrid) == State.OBSTACLE)
            return false;

        return true;
    }
}
