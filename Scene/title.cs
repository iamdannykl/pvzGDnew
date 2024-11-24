using System.IO;
using Godot;
using GodotProjects.code;

namespace GodotProjects.Scene;

public partial class title : Node2D
{
    public static title Instance;
    public ConfigFile configFile = new ConfigFile();
    public override void _Ready()
    {
        Instance = this;
        //string userDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        string userDir = ProjectSettings.GlobalizePath("user://");
        string folderPath = Path.Combine(userDir, "PVZgd");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            GD.Print("Folder created: " + folderPath);
        }
        else
        {
            GD.Print("Folder already exists: " + folderPath);
        }
        string configPath = "user://config.cfg"; // 使用"user://"前缀以在用户目录中创建配置文件
        //configFile.Save(configPath);
        configFile.Load(configPath);
        if (configFile.GetValue("grassDay", "num", "0").ToString() == "0")
            configFile.SetValue("grassDay", "num", 0);
        if (configFile.GetValue("grassNight", "num", "0").ToString() == "0")
            configFile.SetValue("grassNight", "num", 0);
        if (configFile.GetValue("poolDay", "num", "0").ToString() == "0")
            configFile.SetValue("poolDay", "num", 0);
        if (configFile.GetValue("poolNight", "num", "0").ToString() == "0")
            configFile.SetValue("poolNight", "num", 0);
        if (configFile.GetValue("roof", "num", "0").ToString() == "0")
            configFile.SetValue("roof", "num", 0);
        configFile.Save(configPath);

        if (!Directory.Exists(Path.Combine(folderPath, "grassDay")))
            Directory.CreateDirectory(Path.Combine(folderPath, "grassDay"));
        if (!Directory.Exists(Path.Combine(folderPath, "grassNight")))
            Directory.CreateDirectory(Path.Combine(folderPath, "grassNight"));
        if (!Directory.Exists(Path.Combine(folderPath, "poolDay")))
            Directory.CreateDirectory(Path.Combine(folderPath, "poolDay"));
        if (!Directory.Exists(Path.Combine(folderPath, "poolNight")))
            Directory.CreateDirectory(Path.Combine(folderPath, "poolNight"));
        if (!Directory.Exists(Path.Combine(folderPath, "roof")))
            Directory.CreateDirectory(Path.Combine(folderPath, "roof"));
    }
    public void addGq(guanQiaType gtp, int gqs)
    {
        switch (gtp)
        {
            case guanQiaType.grassDay:
                configFile.SetValue("grassDay", "num", gqs);
                break;
            case guanQiaType.grassNight:
                configFile.SetValue("grassNight", "num", gqs);
                break;
            case guanQiaType.poolDay:
                configFile.SetValue("poolDay", "num", gqs);
                break;
            case guanQiaType.poolNight:
                configFile.SetValue("poolNight", "num", gqs);
                break;
            case guanQiaType.roof:
                configFile.SetValue("roof", "num", gqs);
                break;
        }
        configFile.Save("user://config.cfg");
    }
    /* public int getGq(guanQiaType gtp)
{
    switch (gtp)
    {
        case guanQiaType.grassDay:
            return (int)configFile.GetValue("grassDay", "num", "0");
        case guanQiaType.grassNight:
            return (int)configFile.GetValue("grassNight", "num", "0");
        case guanQiaType.poolDay:
            return (int)configFile.GetValue("poolDay", "num", "0");
        case guanQiaType.poolNight:
            return (int)configFile.GetValue("poolNight", "num", "0");
        case guanQiaType.roof:
            return (int)configFile.GetValue("roof", "num", "0");
        default:
            return -1;
    }
} */
}