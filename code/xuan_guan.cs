using System.IO;
using System.Linq;
using Godot;
using YamlDotNet.Serialization;

namespace GodotProjects.code;

public partial class xuan_guan : Control
{
    [Export] public ItemList gqList;
    [Export] public OptionButton whichGtype;
    public PackedScene _nextScene;
    public guanQiaType gtp;
    public int currentSelectedType = -1;
    //public ConfigFile configFile = new ConfigFile();
    public void selectGuanQia(int index)//点击第几关
    {
        //GD.Print("index:" + index);
        saveContent readFromFile;
        //string userDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        string userDir = ProjectSettings.GlobalizePath("user://");
        string folderPath = Path.Combine(userDir, "PVZgd");
        StreamReader sr;
        string jsonString;
        _nextScene = GD.Load<PackedScene>("res://GameScene.tscn");
        Node2D nextSceneInstance = _nextScene.Instantiate<Node2D>();
        GetTree().CurrentScene.QueueFree();
        GetTree().Root.AddChild(nextSceneInstance);
        GetTree().CurrentScene = nextSceneInstance;
        nextSceneInstance.GetNode<Control>("Camera2D/CardUI").Visible = true;
        nextSceneInstance.GetNode<ScrollContainer>("Camera2D/zomCardUI/zomCardLeft").Visible = false;
        reciever dataRec = nextSceneInstance.GetNode<reciever>("reciever");
        var deserializer = new DeserializerBuilder().Build();
        switch (currentSelectedType)
        {
            case 0:
                gtp = guanQiaType.grassDay;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "grassDay"), "grassDay" + (index + 1) + ".yaml"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = deserializer.Deserialize<saveContent>(jsonString);
                foreach (var item in readFromFile.waves)
                {
                    GD.Print("wave");
                    foreach (var item2 in item.zrs)
                    {
                        GD.Print("zr:");
                        foreach (var item3 in item2.zomInfos)
                        {
                            GD.Print("zom" + item3.zomType);
                        }
                    }
                }
                break;
            case 1:
                gtp = guanQiaType.grassNight;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "grassNight"), "grassNight" + (index + 1) + ".yaml"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = deserializer.Deserialize<saveContent>(jsonString);
                break;
            case 2:
                gtp = guanQiaType.poolDay;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "poolDay"), "poolDay" + (index + 1) + ".yaml"));
                GD.Print(Path.Combine(Path.Combine(folderPath, "poolDay"), "poolDay" + (index + 1) + ".yaml"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = deserializer.Deserialize<saveContent>(jsonString);
                break;
            case 3:
                gtp = guanQiaType.poolNight;
                sr = new StreamReader(Path.Combine(Path.Combine(Path.Combine(folderPath, "poolNight"), "poolNIght" + (index + 1) + ".yaml")));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = deserializer.Deserialize<saveContent>(jsonString);
                break;
            case 4:
                gtp = guanQiaType.roof;
                sr = new StreamReader(Path.Combine(Path.Combine(folderPath, "roof"), "grassDay" + (index + 1) + ".yaml"));
                jsonString = sr.ReadToEnd();
                sr.Close();
                readFromFile = deserializer.Deserialize<saveContent>(jsonString);
                break;
            default:
                readFromFile = null;
                break;
        }
        dataRec.recieveData(gtp, 50, currentSelectedType, "name", playMode.player, modeInGame.nutRoll);
        dataRec.recvData(readFromFile);//传入关卡信息至接收器
    }
    public void selectType(int index)//选择关卡地图类型
    {
        GD.Print(index);
        currentSelectedType = index;
        //string userDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        string userDir = ProjectSettings.GlobalizePath("user://");
        string folderPath = Path.Combine(userDir, "PVZgd");
        DirectoryInfo d;
        FileSystemInfo[] fsinfos;
        int gqs = 0;
        FileSystemInfo[] sortedInfos;
        switch (index)
        {
            case 0:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "grassDay"));
                fsinfos = d.GetFileSystemInfos();
                // 使用 LINQ 按名称排序
                sortedInfos = fsinfos.OrderBy(f => f.CreationTime).ToArray();
                foreach (FileSystemInfo fsinfo in sortedInfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                        gqs++;
                    }
                }
                //title.Instance.addGq(guanQiaType.grassDay, gqs);
                break;
            case 1:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "grassNight"));
                fsinfos = d.GetFileSystemInfos();
                // 使用 LINQ 按名称排序
                sortedInfos = fsinfos.OrderBy(f => f.CreationTime).ToArray();
                foreach (FileSystemInfo fsinfo in sortedInfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                        gqs++;
                    }
                }
                //title.Instance.addGq(guanQiaType.grassNight, gqs);
                break;
            case 2:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "poolDay"));
                //OS.ShellOpen(Path.Combine(folderPath, "poolDay"));
                fsinfos = d.GetFileSystemInfos();
                // 使用 LINQ 按名称排序
                sortedInfos = fsinfos.OrderBy(f => f.CreationTime).ToArray();
                foreach (FileSystemInfo fsinfo in sortedInfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                        gqs++;
                    }
                }
                //title.Instance.addGq(guanQiaType.poolDay, gqs);
                break;
            case 3:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "poolNight"));
                fsinfos = d.GetFileSystemInfos();
                // 使用 LINQ 按名称排序
                sortedInfos = fsinfos.OrderBy(f => f.CreationTime).ToArray();
                foreach (FileSystemInfo fsinfo in sortedInfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                        gqs++;
                    }
                }
                //title.Instance.addGq(guanQiaType.poolNight, gqs);
                break;
            case 4:
                gqList.Clear();
                d = new DirectoryInfo(Path.Combine(folderPath, "roof"));
                fsinfos = d.GetFileSystemInfos();
                // 使用 LINQ 按名称排序
                sortedInfos = fsinfos.OrderBy(f => f.CreationTime).ToArray();
                foreach (FileSystemInfo fsinfo in sortedInfos)
                {
                    if (!(fsinfo is DirectoryInfo))     //判断是否为文件夹
                    {
                        //Console.WriteLine(fsinfo.Name);
                        gqList.AddItem(fsinfo.Name);
                        gqs++;
                    }
                }
                //title.Instance.addGq(guanQiaType.roof, gqs);
                break;
        }
    }
}