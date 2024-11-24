namespace GodotProjects.code;

public partial class normalZom : Zombie_base
{
	// Called when the node enters the scene tree for the first time.

	public int NutAttackDuanShu
	{
		get => nutAttackDuanShu;
		set
		{
			if(value<0)return;
			if (value == 0)
			{
				zhuangTaiJi.nowJieDuan = 2;
			}
			nutAttackDuanShu = value;
		}
	}
	public override void Knocked()
	{
		NutAttackDuanShu--;
	}
}