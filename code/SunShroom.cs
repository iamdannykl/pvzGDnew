using Godot;

namespace GodotProjects.code;

public partial class SunShroom : baseCard
{
	public override void placed(bool isWaterPlant)
	{
		base.placed(false);
		//GetNode<sunCreator>("sunCreator").timer.Start();
		GetNode<Timer>("Timer").Start();
		GetNode<sunCreator>("sunCreator").first.Start();
	}
	public override void _Ready()
	{
		base._Ready();
		GetNode<sunCreator>("sunCreator").isNormalSun = false;
	}
	public void TurnToBig()
	{
		animPlayer.Play("bigIdle");
		GetNode<AudioStreamPlayer>("turnBig").Play();
		GetNode<sunCreator>("sunCreator").isNormalSun = true;
	}
}