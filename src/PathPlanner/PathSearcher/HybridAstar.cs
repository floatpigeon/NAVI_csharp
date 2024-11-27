using System.Collections.ObjectModel;
using System.Numerics;
using Maps.GridMap;
using PathPlanner.Nodes;

namespace PathPlanner.HybridAstar;
class HybridAstar
{
    public GridMap Map { get; private set; }
    private PriorityQueue<Node, double> OpenList = new PriorityQueue<Node, double>();
    public HybridAstar(GridMap map) { Map = map; }

    public Vector2 PositionInWorldToGrid(Vector2 worldposition, float size)
    {
        int x = (int)(worldposition.X / size + 0.5);
        int y = (int)(worldposition.Y / size + 0.5);
        return new Vector2(x, y);
    }

    public Node? PathSearch(Vector2 Begin, Vector2 End)
    {
        Node BeginNode = new Node(Begin, null, End);
        OpenList.Enqueue(BeginNode, BeginNode.ValueF);

        while (OpenList.Count != 0)
        {
            Node CurrentNode = OpenList.Dequeue();
            Vector2 CurrentGrid = PositionInWorldToGrid(CurrentNode.Position, 1);

            if (Map.GetState(CurrentGrid) == State.CLOSED)
                continue;

            if (CurrentNode.Position == End)
                return CurrentNode;

            Map.StateChange(CurrentNode.Position, State.CLOSED);
            Collection<Node> Children = CurrentNode.SreachChildren(1.8f, 36, End);

            for (int i = 0; i < Children.Count; i++)
            {
                if (Accessible(Children[i]))
                    OpenList.Enqueue(Children[i], Children[i].ValueF);
            }
        }
        Console.WriteLine("OpenList Empty");
        return null;
    }

    private bool Accessible(Node ChildNode)
    {
        Vector2 ChildGrid = PositionInWorldToGrid(ChildNode.Position, 1);

        if (!Map.IsValid(ChildGrid))
            return false;
        if (Map.GetState(ChildGrid) == State.CLOSED || Map.GetState(ChildGrid) == State.OBSTACLE)
            return false;

        return true;
    }
}
