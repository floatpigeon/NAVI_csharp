using System.Numerics;
using System.Collections.ObjectModel;
using PathPlanner.Nodes;

namespace Maps;

interface IGridMap
{
    int Rows { get; }
    int Cols { get; }
    int Gridsize { get; }
    void Show();
    bool IsValid(Vector2 position);
    bool StateChange(Vector2 site, State state);
    State GetState(Vector2 site);
    Vector2 PositionInWorldToGrid(Vector2 WorldPotion);
    void PathUpdate(INode EndNode);
}
