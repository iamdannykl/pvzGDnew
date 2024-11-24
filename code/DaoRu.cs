using System.IO;
using Godot;

namespace GodotProjects.code;

public partial class DaoRu : Button
{
    [Export] public FileDialog fileDialog;
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(daoRu)));
    }
    void daoRu()
    {
        fileDialog.Popup();
    }
    void daoru(string str)
    {
        GD.Print(str);
    }
    void _on_file_dialog_file_selected(string path)
    {
        GD.Print(path);
        StreamReader reader = new StreamReader(path);
        string conStr = reader.ReadToEnd();
        reader.Close();
        GD.Print(conStr);
    }
}