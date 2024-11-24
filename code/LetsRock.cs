using Godot;

namespace GodotProjects.code;

public partial class LetsRock : Button
{
	plantSelect lftCard;
	void startCreateZom()
	{
		createZom.Instance.startTheTimer();
		GetTree().CurrentScene.GetNode<sunCreator>("sunCrtor").first.Start();
		GetTree().CurrentScene.GetNode<AnimationPlayer>("Camera2D/AnimationPlayer").Play("leftMove");
		GetTree().CurrentScene.GetNode<AnimationPlayer>("Camera2D/UIplantSelect/AnimationPlayer").Play("down");
		lftCard = GetTree().CurrentScene.GetNode<plantSelect>("Camera2D/CardUI/zuoKaCao");
		foreach (Node2D item in lftCard.GetChildren())
		{
			if (item.GetChildCount() > 0)
			{
				item.GetChild<clickButton>(0).isReadyToPlace = true;
				GD.Print("isReadyToPlace" + item.GetChild<clickButton>(0).isReadyToPlace);
			}
		}
	}
}