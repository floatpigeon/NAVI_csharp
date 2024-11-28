using System.Numerics;
using System.Collections.ObjectModel;
using PathPlanner.Nodes;

namespace Maps;

interface IGridMap
{
    int Rows { get; }
    int Cols { get; }
    State[,] Grid { get; }
    void Show();
    bool IsValid(Vector2 position);
    State GetState(Vector2 site);
    bool StateChange(Vector2 site, State state);
    void PathUpdate(Collection<INode> path);
}
