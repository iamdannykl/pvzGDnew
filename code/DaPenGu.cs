using Godot;

namespace GodotProjects.code;

public partial class DaPenGu : baseCard
{
    [Export] public AudioStreamPlayer pen;
    public override void shootBlt()
    {
        base.shootBlt();
        pen.Play();
    }
}