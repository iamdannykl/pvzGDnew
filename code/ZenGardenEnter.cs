using Godot;

namespace GodotProjects.code;

public partial class ZenGardenEnter : TextureButton
{
	PackedScene zenGarden;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		zenGarden = GD.Load<PackedScene>("res://Scene/zenGarden/zen_garden.tscn");
	}

	public void enterZenGarden()
	{
		ZenGardenManager zenGardenIns = zenGarden.Instantiate() as ZenGardenManager;
		GetTree().CurrentScene.QueueFree();
		GetTree().Root.AddChild(zenGardenIns);
		GetTree().CurrentScene = zenGardenIns;
	}
}