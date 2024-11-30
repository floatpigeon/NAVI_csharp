using System.Collections.ObjectModel;
using System.Numerics;

namespace PathPlanner.Nodes;

class Node : INode
{
    const float step = 1.6f;
    const float branch = 36;
    public Node(Vector2 Positon, Node? parent)
    {
        WorldPosition = Positon;
        Parent = parent;
        CalcValueG();
        CalcValueH();
    }
    public Node(float x, float y, INode? parent)
    {
        WorldPosition = new Vector2(x, y);
        Parent = parent;
        CalcValueG();
        CalcValueH();
    }
    public static Vector2 EndPosition;
    public Vector2 WorldPosition { get; private set; }
    public INode? Parent { get; set; }
    public double TotalCost => ValueG + ValueH;
    public double Angle { get; set; }
    private double ValueG { get; set; }
    private double ValueH { get; set; }
    public Collection<INode> SearchChildren
    {
        get
        {
            Collection<INode> children = new();
            if (this.ValueH <= step)
            {
                Console.WriteLine("Find it");
                Console.WriteLine(WorldPosition);

                Node EndNode = new(EndPosition, this);
                children.Add(EndNode);
                return children;
            }
            for (int i = 0; i < branch; i++)
            {
                // Console.WriteLine("Searching");
                double angle = i / branch * 2.0 * Math.PI;
                var (Sin, Cos) = Math.SinCos(angle * step);
                children.Add(new Node(WorldPosition + step * new Vector2((float)Sin, (float)Cos), this));
            }
            return children;
        }
    }
    private void CalcValueG()
    {
        if (Parent == null) { ValueG = 0; }
        else
        {
            double errorAngle = Math.Abs(Angle - Parent.Angle);
            double turnPunish = Math.Log(1 + errorAngle);
            ValueG = step + 0.2 * turnPunish + Parent.TotalCost;
        }
    }
    private void CalcValueH()
    {
        double errorX = WorldPosition.X - EndPosition.X;
        double errorY = WorldPosition.Y - EndPosition.Y;
        ValueH = Math.Sqrt(errorX * errorX + errorY * errorY);
        // Console.WriteLine("{0},{1},{2}", errorX, errorY, ValueH);
        // Console.WriteLine(WorldPosition);
    }

}
