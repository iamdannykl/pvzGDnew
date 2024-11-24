using Godot;

namespace GodotProjects.code;

public partial class quit : Button
{
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(quitGame)));
    }
    void quitGame()
    {
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
        GetTree().Quit();
    }
}