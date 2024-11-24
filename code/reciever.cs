using Godot;

namespace GodotProjects.code;

public enum playMode
{
    player,
    edit,
    noneMode
}
public enum modeInGame
{
    normal,
    paiShanDaoHai,
    nutRoll,
    jianBuKeCui,
    noWay
}
public partial class reciever : Label
{
    public static reciever Instance;
    public guanQiaType thisGuanQiaType;
    public playMode playMode;
    public int sunNum;
    public int RcHangShu;
    public int RcmapIndex;
    public PackedScene mapScene;
    public string GQname;
    public Node2D map2d;
    public saveContent gqData;
    public modeInGame modePublic;
    HBoxContainer hBoxContainer;
    int currentFlagNum;
    public override void _Ready()
    {
        Instance = this;
        hBoxContainer = GetTree().Root.GetNode<HBoxContainer>("GameScene/Camera2D/zomBar/HBoxContainer");
    }
    //recieve and init
    public void recieveData(guanQiaType guanQiaType, int sunOriginalNum, int mapIndex, string gqName, playMode modeInGame, modeInGame modeIG)
    {
        scene.Instance.xianShiUi(modeInGame);
        modePublic = modeIG;
        playMode = modeInGame;
        GD.Print("RC:" + playMode);
        thisGuanQiaType = guanQiaType;
        sunNum = sunOriginalNum;
        RcmapIndex = mapIndex;
        GQname = gqName;
        if (RcmapIndex == 0 || RcmapIndex == 1 || RcmapIndex == 4)
        {
            RcHangShu = 5;
        }
        else if (RcmapIndex == 2 || RcmapIndex == 3)
        {
            RcHangShu = 6;
        }
        GD.Print(guanQiaType);
        switch (guanQiaType)
        {
            case guanQiaType.grassDay:
                mapScene = GD.Load<PackedScene>("res://anim/backGround/grass.tscn");
                break;
            case guanQiaType.grassNight:
                //mapScene = GD.Load<PackedScene>();
                break;
            case guanQiaType.poolDay:
                mapScene = GD.Load<PackedScene>("res://Scene/pool_day.tscn");
                break;
            case guanQiaType.poolNight:
                //mapScene = GD.Load<PackedScene>();
                break;
            case guanQiaType.roof:
                //mapScene = GD.Load<PackedScene>();
                break;
        }
        GD.Print(mapScene.ResourcePath);
        map2d = mapScene.Instantiate() as Node2D;
        GD.Print(map2d.GlobalPosition);
        GetTree().CurrentScene.AddChild(map2d);
        map2d.GlobalPosition = new Vector2(0, -2.5f);
        GD.Print(map2d.GlobalPosition);
        GetTree().Root.GetNode<Label>("GameScene/Camera2D/guanQiaName").Text = GQname;
    }
    public void recvData(saveContent data)
    {
        gqData = data;
        loadFlag();
    }
    void loadFlag()
    {
        foreach (wave wave in gqData.waves)
        {
            if (wave.isBigWave)
            {
                currentFlagNum++;
                (hBoxContainer.GetChild(currentFlagNum - 1) as TextureRect).Visible = true;
            }
        }
    }
}