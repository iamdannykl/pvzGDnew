using Godot;

namespace GodotProjects.code;

public partial class stateMC : AnimationTree
{
    [Export] public bool isBegin;
    [Export] public bool isEat;
    [Export] public bool isDb;
    [Export] public bool isDead;
    [Export]public int nowJieDuan;
    public override void _Ready()
    {
    }
}