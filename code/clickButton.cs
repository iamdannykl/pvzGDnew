using System.Threading.Tasks;
using Godot;

namespace GodotProjects.code;

[Tool]
public partial class clickButton : TextureButton
{
    private PackedScene plantPrefab;
    private baseCard plantInstan;
    private baseCard shadow;
    [Export] public Vector2 UIpianYI;
    [Export] public PlantType plantType;
    [Export] public int sunCost;
    /// <summary>
    /// 左上正，右下负
    /// </summary>
    [Export] public Vector2 offset;
    /// <summary>
    /// 右下正，左上负
    /// </summary>
    [Export] public Vector2 heYeOffSet;
    [Export] public float cd;
    [Export] public bool isShuiSheng;
    [Export] public bool isTaoLei;
    [Export] public bool 是否为咖啡豆类;
    [Export] public bool isReadyToPlace;
    TextureProgressBar mask;
    bool canIclick;
    public bool canFly;
    public bool isOn;
    public bool isInList;
    public bool isAndroidMode;
    public bool isGrey;
    int selfIndex;
    Vector2 bili;
    bool sunEnough;
    bool coldEnough = true;
    bool isplanted = false;
    int currentSun;
    private bool wantPlace;
    private bool canPlace;
    bool isWhiteCard = true;
    int leftCaoCardNum;
    Node2D selectBoard;
    public GridS grid;
    plantSelect lftCard;
    float currentTimeInCD;
    clickButton greyCard;
    public int CurrentSun
    {
        get => currentSun;
        set
        {
            if (currentSun == value) return;
            currentSun = value;
            if (currentSun >= sunCost)
            {
                sunEnough = true;
                SelfModulate = new Color(1, 1, 1, 1);
            }
            else
            {
                sunEnough = false;
                SelfModulate = new Color(0.65f, 0.65f, 0.65f, 1);
            }
        }
    }
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                plantPrefab = resPlantAndZom.Instance.matchPlant(plantType);
                if (IsInstanceValid(plantInstan))
                {
                    plantInstan.Free();
                }
                plantInstan = plantPrefab.Instantiate() as baseCard;
                plantInstan.Monitorable = false;
                plantInstan.Monitoring = false;
                plantInstan.ZIndex = 7;
                plantInstan.CollisionLayer = 0;
                if (IsInstanceValid(shadow) && !shadow.isplanted)
                {
                    shadow.Free();
                }
                shadow = plantPrefab.Instantiate() as baseCard;
                shadow.ZIndex = 2;
                shadow.Modulate = new Color(1, 1, 1, 0.55f);
                shadow.GlobalPosition = new Vector2(0, 0);
                shadow.Visible = false;
                /* shadow.Monitorable = false;
            shadow.Monitoring = false; */
                shadow.CollisionLayer = 0;
                GetTree().CurrentScene.AddChild(shadow);
                Vector2 pos = GlobalPosition;
                plantInstan.GlobalPosition = pos;
                GetTree().CurrentScene.AddChild(plantInstan);
            }
            else
            {
                isplanted = false;
                GD.Print("plant:");
                GD.Print(plantInstan != null);
                if (plantInstan != null)
                {
                    plantInstan.QueueFree();
                    plantInstan = null;
                }
            }
        }
    }
    public void deleteShadow()
    {
        if (shadow != null)
        {
            shadow.QueueFree();
            shadow = null;
        }
    }
    public void initIt()
    {
        WantPlace = false;
    }
    public override void _Ready()
    {
        if (!Engine.IsEditorHint())
        {
            mask = GetNode<TextureProgressBar>("TextureProgressBar");
            loadZT();
            //Connect("button_down", new Callable(this, nameof(_on_button_down)));
            Connect("button_up", new Callable(this, nameof(_on_button_up)));
            Connect("button_down", new Callable(this, nameof(_on_button_down)));
            Connect("mouse_entered", new Callable(this, nameof(mouse_entered)));
            Connect("mouse_exited", new Callable(this, nameof(mouse_exited)));
            bili = Scale;
        }
    }
    async void loadZT()
    {
        await Task.Delay(500);
        canIclick = true;
        lftCard = GetTree().CurrentScene.GetNode<plantSelect>("Camera2D/CardUI/zuoKaCao");
        selectBoard = GetTree().CurrentScene.GetNode<sortTheCard>("Camera2D/UIplantSelect/Control/GridContainer");
        selfIndex = GetParent<Node2D>().GetIndex();
    }
    void mouse_entered()
    {
        if (!isReadyToPlace) return;
        isOn = true;
        if (sunEnough && coldEnough)
        {
            Scale = bili * 1.1f;
        }
        if (OS.GetName() != "Android" && Input.IsActionPressed("clickIt") && danli.Instance.PlantCard != this)
        {
            if (!WantPlace)
            {
                if (sunEnough && coldEnough)
                {
                    danli.Instance.PlantCard = this;
                    WantPlace = true;
                }
            }
            else
            {
                WantPlace = false;
            }
        }
    }
    void mouse_exited()
    {
        if (!isReadyToPlace) return;
        if (plantInstan != null && Input.IsActionPressed("clickIt"))
        {
            isAndroidMode = true;
        }
        isOn = false;
        Scale = bili;
    }
    void _on_button_down()
    {
        if (!isReadyToPlace) return;
        if (!WantPlace)
        {
            if (sunEnough && coldEnough)
            {
                danli.Instance.PlantCard = this;
                WantPlace = true;
            }
        }
        else
        {
            WantPlace = false;
        }
    }
    void _on_button_up()
    {
        if (isReadyToPlace)
        {
            if (isAndroidMode && isOn)
            {
                WantPlace = false;
                isAndroidMode = false;
            }
        }
        else if (canFly)
        {
            if (!isInList)
            {
                if (lftCard != null && canIclick && ((!isGrey) || IsInstanceValid(greyCard) && !greyCard.isGrey) && lftCard.waitPlantCards.Count < lftCard.MAXcardNUm)
                {
                    if (isWhiteCard)
                    {
                        isWhiteCard = false;
                        greyCard = Duplicate() as clickButton;
                        GetParent<Node2D>().AddChild(greyCard);
                        greyCard.isGrey = true;
                        greyCard.isWhiteCard = false;
                        greyCard.Modulate = new Color(0.7f, 0.7f, 0.7f);
                    }
                    flyToLeftCardCao();
                }
            }
            else
            {
                backToSelect();
            }
        }
    }
    void flyToLeftCardCao()
    {
        Tween tween = CreateTween();
        isInList = true;
        Vector2 oriPos = GlobalPosition;
        GD.Print("cardGlp1:" + GlobalPosition);
        Visible = false;
        GetParent<Node2D>().RemoveChild(this);
        ZIndex += 1;
        lftCard.waitPlantCards.Add(this);
        leftCaoCardNum = lftCard.waitPlantCards.Count - 1;
        Node2D itemC = lftCard.GetChild<Node2D>(leftCaoCardNum);
        itemC.AddChild(this);
        GlobalPosition = oriPos;
        Visible = true;
        Vector2 tarPos = itemC.GlobalPosition - new Vector2(UIpianYI.X, UIpianYI.Y);
        GD.Print("oriPos:" + oriPos);
        Vector2 dicVec2 = (tarPos - oriPos).Normalized();
        float dis = (tarPos - oriPos).Length();
        float timeLft = 0.2f;
        GD.Print("tar:" + tarPos);
        GD.Print("pos:" + GlobalPosition);
        tween.TweenProperty(this, "global_position", tarPos, timeLft);
        ZIndex -= 1;
        isInList = true;
    }
    void backToSelect()
    {
        Tween tween = CreateTween();
        isInList = false;
        isReadyToPlace = false;
        Vector2 oriPos = GlobalPosition;
        Node2D node2D = GetParent<Node2D>();
        Vector2 fatherPos = node2D.GlobalPosition;
        Visible = false;
        node2D.RemoveChild(this);
        ZIndex += 1;
        Node2D itemC = selectBoard.GetChild<Node2D>(selfIndex);//待改<==============
        itemC.AddChild(this);
        GlobalPosition = oriPos;
        Visible = true;
        Vector2 tarPos = itemC.GlobalPosition + itemC.GetChild<clickButton>(0).Position/*  - new Vector2(UIpianYI.X, UIpianYI.Y) */;
        GD.Print("index:" + selfIndex);
        GD.Print("tarPos:" + tarPos);
        Vector2 dicVec2 = (tarPos - oriPos).Normalized();
        float dis = (tarPos - oriPos).Length();
        float timeLft = 0.2f;
        tween.TweenProperty(this, "global_position", tarPos, timeLft);
        lftCard.sortCardInList(lftCard.waitPlantCards.IndexOf(this), fatherPos, node2D);
        lftCard.waitPlantCards.Remove(this);
        ZIndex -= 1;
        isInList = false;
    }
    private void CDEnter()
    {
        mask.Value = 1;
        CalCD();
    }
    public float calculateUnitDistance(float juli, float shijian, float unitShijian)
    {
        return juli / shijian * unitShijian;
    }
    async void CalCD()//异步方法计算冷却时间
    {
        float calCD = calculateUnitDistance(1, cd, 0.1f);//最小单位距离=总距离/总时间*最小单位时间
        currentTimeInCD = cd;
        while (currentTimeInCD >= 0)
        {
            Timer timer = GetNode<Timer>("Timer");
            timer.Start();
            await ToSignal(timer, "timeout");
            mask.Value -= calCD;
            currentTimeInCD -= 0.1f;
        }
        coldEnough = true;
    }
    public void PlantIt(baseCard plant)
    {
        if (Input.IsActionJustReleased("clickIt") && GridSys.Instance.isOut == false)//鼠标松开触发
        {
            isAndroidMode = false;
            coldEnough = false;
            CDEnter();
            plant.QueueFree();
            plantInstan = null;
            shadow.isplanted = true;
            shadow.Monitorable = true;
            shadow.Monitoring = true;
            if (plantType == PlantType.cherryBaoDan)
            {
                shadow.ZIndex = 4;
            }
            shadow.CollisionLayer = 1;
            isplanted = true;
            shadow.Modulate = new Color(1, 1, 1, 1);
            shadow.gridS = grid;
            if (grid.plantsOnThisGrid.Count == 0)
            {
                shadow.GlobalPosition = grid.Position - offset;
                shadow.ZIndex = 1;
            }
            else
            {
                /* shadow.GlobalPosition = grid.plantsOnThisGrid[grid.plantsOnThisGrid.Count - 1].GlobalPosition; */
                shadow.ZIndex = 0;
                shadow.GetParent<scene>().RemoveChild(shadow);
                grid.plantsOnThisGrid[grid.plantsOnThisGrid.Count - 1].GetNode<Sprite2D>("Sprite2D").AddChild(shadow);
                shadow.Position = heYeOffSet;
            }
            shadow.placed(isShuiSheng);
            if (!shadow.isRoll)
            {
                grid.plantsOnThisGrid.Add(shadow);
            }
            else
            {
                shadow.ZIndex += 1;
            }
            WantPlace = false;
            if (plantType == PlantType.heYe)
            {
                grid.isHeYe = true;
            }
            SunClct.Instance.SunNum -= sunCost;
        }
    }
    public override void _Process(double delta)
    {
        if (!Engine.IsEditorHint())
        {
            if (isReadyToPlace)
            {
                CurrentSun = SunClct.Instance.SunNum;
            }
            if (isReadyToPlace && WantPlace && plantInstan != null)
            {
                plantInstan.GlobalPosition = GetGlobalMousePosition();//跟随鼠标位置
                grid = GridSys.Instance.GetGridByMouse();
                if ((grid != null) && (
                        (grid.gtp == HangType.water && isShuiSheng && !grid.isHeYe) ||
                        (grid.gtp == HangType.water && !isShuiSheng && grid.isHeYe && grid.plantsOnThisGrid.Count == 1) ||
                        (grid.gtp == HangType.grass && !isShuiSheng && grid.plantsOnThisGrid.Count == 0)
                    ))
                {
                    shadow.Visible = true;
                    shadow.GlobalPosition = grid.Position - offset;
                    PlantIt(plantInstan);
                }
                else
                {
                    shadow.Visible = false;
                }
            }
        }
    }
}