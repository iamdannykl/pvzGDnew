using Godot;

namespace GodotProjects.code;

public partial class soundManager : Panel
{
	// Called when the node enters the scene tree for the first time.
	int BGMindex;
	int effectsIndex;
	int masterIndex;
	public override void _Ready()
	{
		BGMindex = AudioServer.GetBusIndex("Bgm");
		effectsIndex = AudioServer.GetBusIndex("Effects");
		masterIndex = AudioServer.GetBusIndex("Master");
	}
	public void setBgmValue(float volume)
	{
		AudioServer.SetBusVolumeDb(BGMindex, Mathf.LinearToDb(volume));
	}
	public void setEffectsValue(float volume)
	{
		AudioServer.SetBusVolumeDb(effectsIndex, Mathf.LinearToDb(volume));
	}
	public void setMasterValue(float volume)
	{
		AudioServer.SetBusVolumeDb(masterIndex, Mathf.LinearToDb(volume));
	}
}