using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;
public partial class HpJianCe : Node2D
{
    [Export] public int[] hpJieDuan;
    [Export] public int[] isImpJD;//是不是重要阶段
    [Export] public int jieDuanShu;
    [Export] public int NowJieDuan;//为0时是满状态，为(总阶段数-1)时为die
    public override void _Ready()
    {
        jieDuanShu = hpJieDuan.Length;
    }
    public int JieDuanJianCe(int hp)
    {
        for (int i = 1; i <= jieDuanShu; i++)
        {
            if (hp > hpJieDuan[i])
            {
                return i - 1;
            }
            else if (hp > 0 && hp <= hpJieDuan[i])
            {
                continue;
            }
            else
            {
                return jieDuanShu - 1;
            }
        }
        return 0;
    }

}