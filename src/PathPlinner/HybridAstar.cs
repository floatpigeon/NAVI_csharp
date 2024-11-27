using System.Numerics;
using Maps;

namespace PathPlanner
{
    class HybridAstar
    {
        public GridMap Map { get; private set; }
        private PriorityQueue<Node, double> OpenList = new PriorityQueue<Node, double>();
        public HybridAstar(GridMap map)
        {
            Map = map;
        }

        public Vector2 PositionInWorldToGrid(Vector2 worldposition, float size)
        {
            int x = (int)(worldposition.X / size + 0.5);
            int y = (int)(worldposition.Y / size + 0.5);
            return new Vector2(x, y);
        }
        
    }
}
