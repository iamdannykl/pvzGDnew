using Godot;
using GodotProjects.code;

namespace GodotProjects;

public partial class scene : Node2D
{
    public static scene Instance;
    public override void _Ready()
    {
        Instance = this;
    }
    public void xianShiUi(playMode InMode)
    {
        if (InMode == playMode.player)
        {
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/CardUI").Visible = true;
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/zomCardUI").Visible = false;
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/UIplantSelect").Visible = true;
            GetTree().Root.GetNode<AnimationPlayer>("GameScene/Camera2D/AnimationPlayer").Play("rightMove");
        }
        else if (InMode == playMode.edit)
        {
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/CardUI").Visible = false;
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/zomCardUI").Visible = true;
            GetTree().Root.GetNode<Control>("GameScene/Camera2D/UIplantSelect").Visible = false;
        }
    }
}