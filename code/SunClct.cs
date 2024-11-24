using Godot;

namespace GodotProjects.code;

public partial class SunClct : Sprite2D
{
    public static SunClct Instance;
    [Export] public int sunNum;
    Label label;
    public int SunNum
    {
        get => sunNum;
        set
        {
            if (value > 99999)
            {
                sunNum = 99999;
            }
            else
            {
                sunNum = value;
            }
            label.Text = sunNum.ToString();
        }
    }
    public override void _Ready()
    {
        Instance = this;
        label = GetNode<Label>("Label");
        label.Text = sunNum.ToString();
    }
    void getSun(Area2D area2D)
    {
        SunNum += (area2D as sun).sunValue;
        area2D.GetNode<Timer>("Timer").Start();
        area2D.Visible = false;
    }
}