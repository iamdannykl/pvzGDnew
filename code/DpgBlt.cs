using Godot;

namespace GodotProjects.code;

public partial class DpgBlt : bulletBase
{
    public override void atkZom(Area2D zom)
    {
        base.atkZom(zom);
        if (zombieB.Hp > 0)
        {
            zombieB.Hp -= atkValue;
        }
    }
}