using System.Numerics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace PathPlanner
{
    class Node
    {
        public Vector2 Position { get; }
        public Node? Parent { get; }
        public double ValueH { get; private set; }
        public double ValueG { get; private set; }
        public double ValueF => ValueG + ValueH;
        public double Angle { get; private set; } = 0;
        public Node(float x, float y, Node? parent, Vector2 end)
        {
            Position = new Vector2(x, y);
            Parent = parent;
            CalcValueG();
            CalcValueH(end);
        }
        public Node(Vector2 position, Node? parent, Vector2 end)
        {
            Position = position;
            Parent = parent;
            CalcValueG();
            CalcValueH(end);
            // ValueF = ValueG + ValueH;
        }
        public Collection<Node> SreachChildren(float step, int branch, Vector2 end)
        {
            Collection<Node> children = new Collection<Node>();
            if (this.ValueH <= step)
            {
                Node endNode = new Node(end, this, end);
                children.Add(endNode);
                Console.WriteLine("Find It");
                return children;
            }

            for (int i = 0; i < branch; i++)
            {
                double angle = (double)i / branch * 2.0 * Math.PI;
                Vector2 delta = new Vector2((float)Math.Cos(angle) * step, (float)Math.Sin(angle) * step);
                Node child = new Node(Position + delta, this, end);
                child.Angle = angle;
                children.Add(child);
                Console.WriteLine("{0},{1}", child.Position, child.Angle / Math.PI);
            }

            return children;
        }
        private void CalcValueG()
        {
            if (Parent == null) { ValueG = 0; }
            else
            {
                double errorX = Position.X - Parent.Position.X;
                double errorY = Position.Y - Parent.Position.Y;
                double errorAngle = Math.Abs(Angle - Parent.Angle);
                double turnPunish = Math.Log(1 + errorAngle);
                ValueG = Math.Sqrt(errorX * errorX + errorY * errorY) + 0.2 * turnPunish + Parent.ValueG;
            }
        }
        private void CalcValueH(Vector2 end)
        {
            double errorX = Position.X - end.X;
            double errorY = Position.Y - end.Y;
            ValueH = Math.Sqrt(errorX * errorX + errorY * errorY);
        }
    }

    class NodeComparer : IComparer<Node>
    {
        public int Compare(Node? x, Node? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("Node objects cannot be null");
            }
            return x.ValueF.CompareTo(y.ValueF);
        }
    }
}
