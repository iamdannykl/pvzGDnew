using Godot;
using Vector2 = Godot.Vector2;
namespace GodotProjects.code;

public class zomInfo
{
    public Vector2 pos;
    public int hangShu;
    public ZomType zomType;
    public zomInfo(Vector2 wz, ZomType ztp, int hang)
    {
        pos = wz;
        zomType = ztp;
        hangShu = hang;
        GD.Print("i am :" + ztp + zomType);
    }
    public zomInfo()
    {

    }
}