
using System.Drawing;
using g4;

namespace TrajectoryTracer.MotionModel.Models;

interface IModel
{
    double Speed { get; }
    Vector2d Direction { get; }
    void Update();
    void ControlInput(Vector2d input);
    void GetStateMatrix();

}
