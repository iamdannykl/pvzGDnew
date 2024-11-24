using Godot;

namespace GodotProjects.code;

public partial class BigWaveComing : Label
{
    void fuWei()
    {
        Visible = false;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("RESET");
    }
}