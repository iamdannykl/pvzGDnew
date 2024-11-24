using Godot;

namespace GodotProjects.code;

public partial class luZhangZom : Zombie_base
{
    // Called when the node enters the scene tree for the first time.
    public int NutAttackDuanShu
    {
        get => nutAttackDuanShu;
        set
        {
            if(value<0)return;
            if (value == 1)
            {
                zhuangTaiJi.nowJieDuan = 3;
            }

            if (value == 0)
            {
                zhuangTaiJi.nowJieDuan = 5;
            }
            nutAttackDuanShu = value;
        }
    }
    public override void Knocked()
    {
        GD.Print("nut");
        NutAttackDuanShu--;
    }
}