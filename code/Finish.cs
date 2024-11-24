using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Godot;

namespace GodotProjects.code;

public enum guanQiaType
{
    grassDay,
    grassNight,
    poolDay,
    poolNight,
    roof
}
public partial class Finish : Button
{
    private PackedScene _nextScene;
    [Export] public OptionButton mapType;
    [Export] public LineEdit sunOriginal;
    [Export] public OptionButton modeType;
    [Export] public LineEdit guanQiaName;
    public guanQiaType guanQiaType;
    public int mapTypeIndex = -1;
    public int modeTypeIndex = -1;
    Dictionary<int, modeInGame> idxToEnmu;
    public override void _Ready()
    {
        // 加载目标场景
        _nextScene = GD.Load<PackedScene>("res://GameScene.tscn");
        Connect("button_up", new Callable(this, nameof(SwitchScene)));
        mapType.Connect("item_selected", new Callable(this, nameof(getMapIndex)));
        modeType.Connect("item_selected", new Callable(this, nameof(getModeIndex)));
        idxToEnmu = new Dictionary<int, modeInGame>{
            {0,modeInGame.normal},
            {1,modeInGame.paiShanDaoHai},
            {2,modeInGame.nutRoll},
            {3,modeInGame.jianBuKeCui}
        };
    }
    void getMapIndex(int index)
    {
        mapTypeIndex = index;
    }
    void getModeIndex(int index)
    {
        modeTypeIndex = index;
    }
    public void SwitchScene()
    {
        if (!(mapTypeIndex > -1 && Regex.IsMatch(sunOriginal.Text, @"^\d+$") && !string.IsNullOrWhiteSpace(guanQiaName.Text))) return;
        //GetTree().Root.GetNode<xuan_guan>("Title/xuanGuan").gqList.AddItem("sss");
        Node nextSceneInstance = _nextScene.Instantiate();
        GetTree().CurrentScene.QueueFree();
        GetTree().Root.AddChild(nextSceneInstance);
        //nextSceneInstance.GetNode<Label>("Label").Text = "asd";
        GetTree().CurrentScene = nextSceneInstance;
        addDatasToMap(sunOriginal.Text);//给新场景传输数据
        /* nextSceneInstance.GetNode<Control>("Camera2D/CardUI").Visible = false;//cardUI
        nextSceneInstance.GetNode<ScrollContainer>("Camera2D/zomCardUI/zomCardLeft").Visible = true; */
        nextSceneInstance.GetNode<reciever>("reciever").recieveData(guanQiaType, sunOriginal.Text.ToInt(), mapTypeIndex, guanQiaName.Text, playMode.edit, getEnumFromIdx());
        //nextSceneInstance.GetNode<Camera2D>("Camera2D").rightYi();
    }
    modeInGame getEnumFromIdx()
    {
        if (idxToEnmu.TryGetValue(modeTypeIndex, out modeInGame mig))
        {
            return mig;
        }
        return modeInGame.noWay;
    }
    public void addDatasToMap(string sunStr)
    {
        if (mapTypeIndex > -1 && Regex.IsMatch(sunStr, @"^\d+$"))
        {
            switch (mapTypeIndex)
            {
                case 0:
                    guanQiaType = guanQiaType.grassDay;
                    break;
                case 1:
                    guanQiaType = guanQiaType.grassNight;
                    break;
                case 2:
                    guanQiaType = guanQiaType.poolDay;
                    break;
                case 3:
                    guanQiaType = guanQiaType.poolNight;
                    break;
                case 4:
                    guanQiaType = guanQiaType.roof;
                    break;
            }
        }
        else
        {

        }
    }
}