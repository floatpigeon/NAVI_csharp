using System.Collections.ObjectModel;
using System.Numerics;

namespace PathPlanner.Nodes;

internal interface INode
{
    Vector2 WorldPosition { get; }
    INode? Parent { get; }
    double TotalCost { get; }
    double Angle { get; }
    Collection<INode> SearchChildren { get; }
}
