using System.Numerics;
using PathPlanner.Nodes;

class Program
{
    static void Main()
    {
        Console.WriteLine("Start");
        Node beginNode = new Node(5, 5, null);
        Node.EndPosition = new Vector2(0, 0);

        PriorityQueue<Node, double> OpenList = new PriorityQueue<Node, double>();

        Node node1 = new Node(1, 0, beginNode);
        Node node2 = new Node(2, 0, beginNode);
        Node node3 = new Node(2, 1, beginNode);
        Node node4 = new Node(2, 2, beginNode);

        OpenList.Enqueue(node1, node1.TotalCost);
        OpenList.Enqueue(node2, node2.TotalCost);
        OpenList.Enqueue(node3, node3.TotalCost);
        OpenList.Enqueue(node4, node4.TotalCost);

        while (OpenList.Count != 0)
        {
            Node nodetop = OpenList.Dequeue();
            Console.WriteLine("{0},{1}", nodetop.WorldPosition, nodetop.TotalCost);
        }

    }
}
