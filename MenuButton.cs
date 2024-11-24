using Godot;

namespace GodotProjects;

public partial class MenuButton : Godot.MenuButton
{
    private PopupMenu _popupMenu;

    public override void _Ready()
    {

        _popupMenu = GetPopup();

        _popupMenu.AddItem("菜单项 1", 0);
        _popupMenu.AddItem("菜单项 2", 1);
        _popupMenu.AddItem("菜单项 3", 2);

        _popupMenu.Connect("id_pressed", new Callable(this, nameof(OnItemPressed)));
    }

    private void OnItemPressed(int id)
    {
        GD.Print($"选择了菜单项: {id}");
    }
}