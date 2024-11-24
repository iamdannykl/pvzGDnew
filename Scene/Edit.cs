using Godot;

namespace GodotProjects.Scene;

public partial class Edit : Button
{
    [Export] public Control start;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(openCreateWindow)));
    }
    void openCreateWindow()
    {
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
        start.GetNode<Button>("Panel/ADD").Visible = true;
        start.GetNode<Label>("Panel/MODE").Text = "EditMode";
        start.GetNode<AnimationPlayer>("AnimationPlayer").Play("up");
    }
}