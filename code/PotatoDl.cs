using System.Collections.Generic;
using Godot;

namespace GodotProjects.code;

public partial class PotatoDl : baseCard
{
	List<Zombie_base> zomList = new List<Zombie_base>();
	bool isMaoChu, isBao;
	[Export] public AudioStreamPlayer boom;
	Timer timer;
	int layerNum;
	/*public override void _Ready(){
	base._Ready();
	layerNum=GetNode<Area2D>("addZom").Getco;
}*/
	public override void placed(bool isWaterPlant)
	{
		base.placed(isWaterPlant);
		timer = GetNode<Timer>("Timer");
		timer.Start();
	}
	public override void desSelfI()
	{
		gridS.plantsOnThisGrid.Remove(this);
		QueueFree();
	}
	void maoChu()
	{
		isMaoChu = true;
		animPlayer.Play("mao");
	}
	void desSelf()
	{
		QueueFree();
	}
	void playBoom()
	{
		boom.Play();
	}
	void idleAnim()
	{
		animPlayer.Play("idle");
		isZiBao = true;
	}
	void addZom(Area2D zom)
	{
		Zombie_base zombie = zom.GetParent<Zombie_base>();
		zombie.InListPotato(this);
		zomList.Add(zombie);
		GD.Print("added!!!");
	}
	public void outQueue(Zombie_base zom)
	{
		zomList.Remove(zom);
	}
	public override void explodeIt()
	{
		GD.Print("startEXPLD!!!");
		if (zomList.Count > 0)
		{
			foreach (var zombie in zomList)
			{
				if (IsInstanceValid(zombie))
				{
					zombie.QueueFree();
					createZom.Instance.AllZomNum++;
				}
			}
		}
		GD.Print("finishEXPLD!!!");
		animPlayer.Play("ni");
	}
}