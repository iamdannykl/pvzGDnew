using Godot;

namespace GodotProjects.code;

public partial class Camera2D : Godot.Camera2D
{
    public static Camera2D Instance;
    [Export] public AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        Instance = this;
    }
    public void rightYi()
    {
        GD.Print("111111111" + GetTree().Root.GetNode<reciever>("GameScene/reciever").playMode);
        if (GetTree().Root.GetNode<reciever>("GameScene/reciever").playMode == playMode.edit)
        {
            GD.Print("right");
            animationPlayer.Play("rightMove");
        }
    }
}