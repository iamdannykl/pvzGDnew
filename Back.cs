using Godot;

namespace GodotProjects;

public partial class Back : Button
{
    [Export] public Control start;
    [Export] public bool isAddWindow;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(closeCreateWindow)));
    }
    void closeCreateWindow()
    {   
        if (isAddWindow)
            start.GetParent<Panel>().GetNode<AnimationPlayer>("AnimationPlayer").Play("down");
        else
            start.GetNode<AnimationPlayer>("AnimationPlayer").Play("down");
    }
}