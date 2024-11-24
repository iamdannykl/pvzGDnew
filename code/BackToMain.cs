using Godot;

namespace GodotProjects.code;

public partial class BackToMain : Button
{
	private void backToMain()
	{
		Scene.title mainScene = GD.Load<PackedScene>("res://Scene/title.tscn").Instantiate() as Scene.title;
		GetTree().CurrentScene.QueueFree();
		GetTree().Root.AddChild(mainScene);
		GetTree().CurrentScene = mainScene;
	}
}