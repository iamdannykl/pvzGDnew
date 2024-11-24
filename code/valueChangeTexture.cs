using Godot;

namespace GodotProjects.code;

public partial class valueChangeTexture : TextureProgressBar
{
    public void changeIt(float value)
    {
        Value = value;
    }
}