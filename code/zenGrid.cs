using Godot;

namespace GodotProjects.code;

public class zenGrid
{
    public bool HasPot;
    public Vector2 Point;
    //世界坐标
    public Vector2 Position;
    public zenGrid(Vector2 point, Vector2 position, bool hasPot)
    {
        Point = point;
        Position = position;
        HasPot = hasPot;
    }
}