using System.Numerics;
using Maps;
using PathPlanner.HybridAstar;
using PathPlanner.Nodes;

class Program
{
    static void Main()
    {
        Console.WriteLine("Start");

        int[,] arr = new int[8, 8]
        {
            {1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1},
            {1,1,1,1,1,0,0,1},
            {1,1,1,1,1,0,0,1},
            {1,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1},
    };

        HybridAstar PathPlanner = new(arr);
        Vector2 begin = new(1, 1);
        Vector2 end = new(6, 1);

        INode? endNode = PathPlanner.PathSearch(begin, end);

        INode? node = endNode;
        while (node != null)
        {
            Console.WriteLine("position:{0}", node.WorldPosition);
            node = node.Parent;
        }
    }
}
