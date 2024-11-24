using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace GodotProjects.code;

public partial class createZom : Sprite2D,IbeKnocked
{
    public static createZom Instance;
    public saveContent loadData;
    public static object lockObject = new object();
    [Export] public Timer waveTimer;
    [Export] public int eachBoJianGe;
    public List<BigWave> bigWaves = new List<BigWave>();
    List<wave> zanCunWave = new List<wave>();
    public bool isUPDT;
    public int allZomNum, allCrtZom;
    public int waveNow = -1;
    int crtWave;
    public float timePerWave;
    private float nowTime;
    int bigWaveCount;
    float currentTimeInCD;
    GridSys gridSys;
    TextureProgressBar greenBar;
    PathFollow2D pathFollow2D;
    [Export] public Timer startTimer;
    Timer timer;
    Timer check;
    bool isEnterNext;
    bool tst;
    float inlc;
    Label bigWaveComing;
    AnimationPlayer bigWaveAnim;
    Label testLable, te2;
    bool isOk;
    int crtBo = 1;
    private int boshu;
    private static object locker = new object();
    public override void _Ready()
    {
        Instance = this;
        gridSys = GetNode<GridSys>("GridSys");
        timer = GetNode<Timer>("zomBarTimer");
        bigWaveComing = GetTree().Root.GetNode<Label>("GameScene/Camera2D/BigWaveComing");
        bigWaveAnim = bigWaveComing.GetNode<AnimationPlayer>("AnimationPlayer");
        check = GetNode<Timer>("check");
        te2 = GetTree().Root.GetNode<Label>("GameScene/Camera2D/Label2");
        testLable = GetTree().Root.GetNode<Label>("GameScene/Camera2D/Label");
        greenBar = GetTree().Root.GetNode<TextureProgressBar>("GameScene/Camera2D/zomBar/TextureProgressBar");
        pathFollow2D = GetTree().Root.GetNode<PathFollow2D>("GameScene/Camera2D/zomBar/Path2D/PathFollow2D");

    }
    public override void _Process(double delta)
    {
        testLable.Text = "allCrtZom" + allCrtZom.ToString();
        te2.Text = "allZomNum" + allZomNum.ToString();
    }
    void loadBigWaves()
    {
        bigWaveCount = 0;
        foreach (wave currentWave in loadData.waves)
        {
            if (!currentWave.isBigWave)
            {
                zanCunWave.Add(currentWave);
            }
            else
            {
                BigWave bigWave = new BigWave(currentWave)
                {
                    miniWaves = new List<wave>(zanCunWave)
                };
                bigWaveCount++;
                zanCunWave.Clear();
                bigWave.bigWave = currentWave;
                bigWaves.Add(bigWave);
            }
        }
        for (int i = 0; i < bigWaves.Count; i++)
        {
            bigWaves[i].bigWave.location = 1.0f / bigWaveCount * (i + 1);
            GD.Print("bigLoc:" + bigWaves[i].bigWave.location + bigWaveCount + (i + 1) + (1.0f / 1 * 1) + 1.0f / bigWaveCount * (i + 1));
            for (int j = 0; j < bigWaves[i].miniWaves.Count; j++)
            {
                if (i > 0)
                {
                    bigWaves[i].miniWaves[j].location = (bigWaves[i].bigWave.location - bigWaves[i - 1].bigWave.location) / (bigWaves[i].miniWaves.Count + 1) * (j + 1) + bigWaves[i - 1].bigWave.location;
                }
                else
                {
                    if (j == 0)
                    {
                        bigWaves[i].miniWaves[j].location = 0f;
                    }
                    bigWaves[i].miniWaves[j].location = bigWaves[i].bigWave.location / bigWaves[i].miniWaves.Count * j;
                }
            }
        }
        foreach (wave wave in loadData.waves)
        {
            GD.Print("waveLocation:" + wave.location);
        }
    }
    async void zomBarRun(float inLocation)//僵尸进度条缓慢前进
    {
        boshu++;
        currentTimeInCD = (float)waveTimer.WaitTime;
        GD.Print("curTimeCd:" + currentTimeInCD);
        float calCD = inLocation / currentTimeInCD * 0.1f;
        GD.Print("calcd:" + calCD);
        //double crtime = Time.GetUnixTimeFromSystem();
        //double crtBiLi = greenBar.Value;
        double crtBiLi = loadData.waves[crtWave].location;
        crtWave++;
        GD.Print("!isEnterNext:" + !isEnterNext);
        while (currentTimeInCD >= 0 && greenBar.Value < crtBiLi + inLocation && !isEnterNext && greenBar.Value + calCD < 1 && pathFollow2D.ProgressRatio + calCD < 1)
        {
            await Task.Delay(100);
            lock (lockObject)//进程上锁
            {
                if (!(IsInstanceValid(greenBar) && IsInstanceValid(pathFollow2D))) return;
                greenBar.Value += calCD;
                pathFollow2D.ProgressRatio += calCD;
                currentTimeInCD -= 0.1f;
            }
        }
        crtBo++;
        double newChaZhi = loadData.waves[crtWave].location - greenBar.Value;
        float newCCD = ((float)newChaZhi) / (eachBoJianGe * 0.6f) * 0.1f;
        while (greenBar.Value < crtBiLi + inLocation)
        {
            await Task.Delay(100);
            lock (lockObject)
            {
                if (!(IsInstanceValid(greenBar) && IsInstanceValid(pathFollow2D))) return;
                if (greenBar.Value + newCCD < crtBiLi + inLocation && greenBar.Value + newCCD < 1 && pathFollow2D.ProgressRatio + newCCD < 1)
                {
                    greenBar.Value += newCCD;
                    pathFollow2D.ProgressRatio += newCCD;
                }
            }
        }
    }
    public void startTheTimer()
    {
        startTimer.Start();
    }
    public void _on_timer_timeout()
    {
        if (reciever.Instance.playMode == playMode.player)
        {
            isUPDT = true;
            loadData = reciever.Instance.gqData;
            loadBigWaves();
            GD.Print("waveNow:" + waveNow);
            GD.Print("zong:" + loadData.waves.Count);
        }
        else
        {
            isUPDT = false;
        }
        if (isUPDT)
        {
            if (waveNow < loadData.waves.Count - 1)
            {
                ifChaoShi();
            }
        }
    }
    async void nxtAsy()
    {
        await Task.Delay(1000 * eachBoJianGe);
        nextWaveAfterWait();
    }
    public int AllZomNum
    {
        get => allZomNum;
        set
        {
            allZomNum = value;
            if (allZomNum >= allCrtZom)
            {
                if (isUPDT)
                {
                    if (waveNow < loadData.waves.Count - 1)
                    {
                        isEnterNext = true;
                        waveTimer.Stop();
                        nxtAsy();
                    }
                    else
                    {
                        GD.Print("Succeeded!");
                    }
                }
            }
        }
    }
    public void nextWaveAfterWait()
    {
        ifChaoShi();
    }
    public void jiShiJieShu()
    {
        if (isUPDT)
        {
            if (waveNow < loadData.waves.Count - 1)
            {
                ifChaoShi();
            }
        }
    }
    async void bigWaveLoad(int num, int zrNum)
    {
        bigWaveComing.Visible = true;
        bigWaveAnim.Play("fangDa");
        waveTimer.Paused = true;
        await Task.Delay(3200);
        lock (lockObject)
        {
            if (!IsInstanceValid(waveTimer)) return;
        }
        waveTimer.Paused = false;
        realCRT(num, zrNum);
    }
    void realCRT(int num, int zrNum)
    {
        for (int i = 0; i < num; i++)
        {
            GD.Print("loadData.waves[waveNow].zrs[zrNum].zomInfos[i]:" + waveNow + zrNum + i);
            zomInfo zif = loadData.waves[waveNow].zrs[zrNum].zomInfos[i];
            gridSys.hangList[zif.hangShu - 1].zomInfos.Add(zif);
            allCrtZom += 1;
        }
        gridSys.LuanXuHang();
        isEnterNext = false;
        if (waveNow < loadData.waves.Count - 1)
        {
            tst = true;
            zomBarRun(loadData.waves[waveNow + 1].location - loadData.waves[waveNow].location);
            GD.Print("wave:" + waveNow);
            GD.Print("loc:" + (loadData.waves[waveNow + 1].location - loadData.waves[waveNow].location));
        }
    }
    void CRTzombies(int num, int zrNum)
    {
        if (loadData.waves[waveNow].isBigWave)
        {
            bigWaveLoad(num, zrNum);
        }
        else
        {
            realCRT(num, zrNum);
        }
    }
    void ifChaoShi()
    {
        int shangBoCrtLeft = allCrtZom - allZomNum;
        allZomNum = 0;
        lock (lockObject)
        {
            if (!IsInstanceValid(waveTimer)) return;
        }
        waveNow++;
        waveTimer.Start();
        nextBo();
        allCrtZom += shangBoCrtLeft;
    }
    void nextZr(int zrnum, int jishu)
    {
        int Znum = loadData.waves[waveNow].zrs[zrnum].zomInfos.Count;
        int jis = jishu;
        if (Znum > 0)
        {
            CRTzombies(Znum, zrnum);
        }
        else
        {
            if (zrnum < 2)
            {
                zrnum++;
            }
            else
            {
                zrnum = 0;
            }
            jis++;
            if (jis < 3)
            {
                nextZr(zrnum, jis);
            }
            else
            {
                GD.Print("ERROR EACH ZR IS NULL!");
            }
        }
    }
    public void nextBo()
    {
        GD.Print("nextBo");
        waveTimer.Start();
        allCrtZom = 0;
        allZomNum = 0;
        RandomNumberGenerator random = new RandomNumberGenerator();
        random.Randomize();
        int suiJi = random.RandiRange(1, 3);
        int jiShu = 0;
        nextZr(suiJi - 1, jiShu);
    }

    public void Knocked()
    {
        
    }
}