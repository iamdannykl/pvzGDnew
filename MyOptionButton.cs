using Godot;

namespace GodotProjects;

public partial class MyOptionButton : OptionButton
{
    public override void _Ready()
    {
        Connect("item_selected", new Callable(this, nameof(OnItemSelected)));
    }

    private void OnItemSelected(int index)
    {
        GD.Print($"选择了选项:" + index);
    }
}