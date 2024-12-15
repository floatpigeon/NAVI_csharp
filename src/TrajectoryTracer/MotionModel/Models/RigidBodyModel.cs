
using System.Numerics;
using g4;

namespace TrajectoryTracer.MotionModel.Models;

class RigidBodyModel : IModel
{
    RigidBodyModel(double m, double r)
    {
        _mass = m;
        _inertia = 0.5 * m * r * r;
        _resistance = 0.01 * m;
    }
    public double Speed { get; private set; }
    public Vector2d Direction { get; private set; }
    public void Update()
    {
    }
    public void ControlInput(Vector2d input)
    {
        _force = Math.Sqrt(input.x * input.x + input.y + input.y);
        double theta = Math.Atan(input.y / input.x);
        _direction = new(Math.Cos(theta), Math.Sin(theta));
    }
    public void GetStateMatrix() { }
    private double _force;
    private Vector2d _direction;
    private double _mass;
    private double _inertia;
    private double _resistance;
};