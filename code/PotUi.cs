using Godot;

namespace GodotProjects.code;

public partial class PotUi : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void anXia()
	{
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
	}
}