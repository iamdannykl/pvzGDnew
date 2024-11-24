using Godot;

namespace GodotProjects.code;

public partial class zomCard : TextureButton
{
    [Export] public ZomType zomType;
    public Zombie_base zomInsta, zomShadow;
    public hang hang;
    OptionButton naniZR;
    int ZRnum = -1;
    bool wantPlace;
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                zomInsta = resPlantAndZom.Instance.matchZom(zomType).Instantiate() as Zombie_base;
                zomShadow = resPlantAndZom.Instance.matchZom(zomType).Instantiate() as Zombie_base;
                zomShadow.Modulate = new Color(1, 1, 1, 0.55f);
                zomShadow.GlobalPosition = new Vector2(0, 0);
                zomShadow.Visible = false;
                if (ZRnum == 0)
                {
                    GetTree().CurrentScene.AddChild(zomShadow);
                    Vector2 pos = GlobalPosition;
                    zomInsta.GlobalPosition = pos;
                    GetTree().CurrentScene.AddChild(zomInsta);
                }
                else if (ZRnum == 1)
                {
                    GetTree().CurrentScene.AddChild(zomShadow);
                    Vector2 pos = GlobalPosition;
                    zomInsta.GlobalPosition = pos;
                    GetTree().CurrentScene.AddChild(zomInsta);
                }
                else if (ZRnum == 2)
                {
                    GetTree().CurrentScene.AddChild(zomShadow);
                    Vector2 pos = GlobalPosition;
                    zomInsta.GlobalPosition = pos;
                    GetTree().CurrentScene.AddChild(zomInsta);
                }
                else
                {
                    WantPlace = false;
                }
                /* GetTree().Root.AddChild(zomShadow);
            Vector2 pos = GlobalPosition;
            zomInsta.GlobalPosition = pos;
            GetTree().Root.AddChild(zomInsta); */
            }
            else
            {
                if (zomInsta != null)
                {
                    zomInsta.QueueFree();
                    zomInsta = null;
                }
            }
        }
    }
    public override void _Ready()
    {
        Connect("button_up", new Callable(this, nameof(placeZom)));
        naniZR = GetTree().Root.GetNode<OptionButton>("GameScene/Camera2D/zomCardUI/Panel/zhenRong");
        naniZR.Connect("item_selected", new Callable(this, nameof(xuanQuZR)));
    }
    void xuanQuZR(int index)
    {
        ZRnum = index;
    }
    void placeZom()
    {
        if (!WantPlace)
        {
            danli.Instance.ZombieCard = this;
            WantPlace = true;
        }
        else
        {
            WantPlace = false;
        }
    }
    void plantZom(int hangNum)
    {
        if (Input.IsActionJustReleased("clickIt") && GridSys.Instance.isOut == false)//鼠标松开触发
        {
            zomInsta.QueueFree();
            zomInsta = null;
            /* - GridSys.Instance.hangList[0].hangTou */
            zomShadow.Modulate = new Color(1, 1, 1, 1);
            zomShadow.weiZhi = zomShadow.GlobalPosition;
            zomShadow.placed(hangNum);
            string zrs = "Camera2D/ZR" + (ZRnum + 1);
            GetTree().CurrentScene.RemoveChild(zomShadow);
            GetTree().CurrentScene.GetNode<Node2D>(zrs).AddChild(zomShadow);
            zomShadow.GlobalPosition = new Vector2(GetGlobalMousePosition().X, hang.hangTou.Y);
            WantPlace = false;
        }
    }
    public override void _Process(double delta)
    {
        if (WantPlace && zomInsta != null)
        {
            zomInsta.GlobalPosition = GetGlobalMousePosition();//跟随鼠标位置
            hang = GridSys.Instance.GetHangByMouse();
            if (hang != null)
            {
                zomShadow.Visible = true;
                zomShadow.GlobalPosition = new Vector2(GetGlobalMousePosition().X, hang.hangTou.Y);
                plantZom(hang.hangShu);
            }
            else
            {
                zomShadow.Visible = false;
            }
        }
    }
}