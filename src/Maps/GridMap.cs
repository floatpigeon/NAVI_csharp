using System.Collections.ObjectModel;
using System.Numerics;
using PathPlanner.Nodes;

namespace Maps;

enum State { UNKNOW, EMPTY, OBSTACLE, PATH, CLOSED, POINT }

class GridMap : IGridMap
{
    const int GridSize = 1;
    public int Rows { get; }
    public int Cols { get; }
    public int Gridsize { get => GridSize; }
    public State[,] Grid { get; private set; }

    public GridMap(int[,] arr)
    {
        Rows = arr.GetLength(0);
        Cols = arr.GetLength(1);
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
            return true;

        else { return false; }
    }
    public bool StateChange(Vector2 site, State state)
    {
        if (IsValid(site))
        {
            Grid[(int)site.X, (int)site.Y] = state;
            return true;
        }
        return false;
    }
    public State GetState(Vector2 site) { return Grid[(int)site.X, (int)site.Y]; }
    public Vector2 PositionInWorldToGrid(Vector2 WorldPosition)
    {
        int x = (int)Math.Round(WorldPosition.X / GridSize, MidpointRounding.AwayFromZero);
        int y = (int)Math.Round(WorldPosition.Y / GridSize, MidpointRounding.AwayFromZero);
        return new Vector2(x, y);
    }
    public void PathUpdate(INode? node)
    {
        while (node != null)
        {
            StateChange(PositionInWorldToGrid(node.WorldPosition), State.PATH);
            node = node.Parent;
        }
    }
    private State[,] Creat(int[,] arr)
    {
        State[,] map = new State[Rows, Cols];
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                map[i, j] = NumToState(arr[i, j]);
            }
        }
        return map;
    }
    private State NumToState(int i)
    {
        return i switch
        {
            0 => State.EMPTY,
            1 => State.OBSTACLE,
            _ => State.UNKNOW,
        };
    }

}
