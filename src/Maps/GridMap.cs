using System.Collections.ObjectModel;
using System.Numerics;
using PathPlanner;

namespace Maps
{
    enum State
    {
        UNKNOW, EMPTY, OBSTACLE, PATH, CLOSED, POINT
    }
    class GridMap
    {
        public int Rows { get; }
        public int Cols { get; }
        public State[,] Grid { get; private set; }
        public GridMap(int row, int col, int[,] arr)
        {
            Rows = row;
            Cols = col;
            Grid = Creat(arr);
        }
        public GridMap(State[,] arr)
        {
            Rows = arr.GetLength(0);
            Cols = arr.GetLength(1);
            Grid = arr;
        }
        public void Show()
        {

        }
        public bool IsValid(Vector2 position)
        {
            if (position.X >= 0 && position.X < Rows && position.Y >= 0 && position.Y < Cols)
            {
                return true;
            }
            else { return false; }
        }
        public void StateChange(Vector2 site, State state)
        {
            if (IsValid(site))
            {
                Grid[(int)site.X, (int)site.Y] = state;
            }
        }
        public void PathUpdate(Collection<Node> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                int x = (int)path[i].Position.X;
                int y = (int)path[i].Position.Y;
                Grid[x, y] = State.PATH;
            }
        }
        private State[,] Creat(int[,] arr)
        {
            State[,] map = new State[Rows, Cols];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; i++)
                {
                    map[i, j] = NumToState(arr[i, j]);
                }
            }
            return map;
        }
        private State NumToState(int i)
        {
            switch (i)
            {
                case 0:
                    return State.EMPTY;
                case 1:
                    return State.OBSTACLE;
            }
            return State.UNKNOW;
        }
    }
}
