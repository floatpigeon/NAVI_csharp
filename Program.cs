using System.Numerics;
using PathPlanner.Node;
using PathPlanner.HybridAstar;

class Program
{
    static void Main()
    {
        Console.WriteLine("Start");
        Vector2 end = new Vector2(3, 2);
        Node mynode = new Node(1, 2, null, end);
        mynode.SreachChildren(1.5f, 4, end);

        Vector2 iv = new Vector2(1.4f, 3.3f);
        HybridAstar a = new HybridAstar();
        Vector2 ov = a.PositionInWorldToGrid(iv, 1.2f);
        Console.WriteLine(ov);
    }
}
