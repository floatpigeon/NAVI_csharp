using System.Numerics;
using PathPlanner.Node;

class Program
{
    static void Main()
    {
        Console.WriteLine("Start");
        Vector2 end = new Vector2(3, 2);
        Node mynode = new Node(1, 2, null, end);

        mynode.SreachChildren(1.5f, 4, end);
    }
}
