using System;
using Godot;

namespace GodotProjects.code;

public partial class tongXin : Button
{
	// Called when the node enters the scene tree for the first time.
	public ENetMultiplayerPeer eNetMultiplayerPeer;
	public override void _Ready()
	{
		//Connect("button_up", new Callable(this, nameof(NetButtonDown)));
		eNetMultiplayerPeer = new ENetMultiplayerPeer();
	}
	public void _on_button_down()
	{
		GD.Print("creating...");
		var error = eNetMultiplayerPeer.CreateServer(1145);
		if (error != Error.Ok)
		{
			GD.Print("Creat failed!");
			return;
		}
		GD.Print("create Succeeded!");
		Multiplayer.MultiplayerPeer = eNetMultiplayerPeer;
		Multiplayer.PeerConnected += OnPeerConnected;
	}
	void buttonDownJoin()
	{
		GD.Print("Ready to join...");
		try
		{
			eNetMultiplayerPeer.CreateClient("192.168.31.165", 1145);
			Multiplayer.MultiplayerPeer = eNetMultiplayerPeer;
		}
		catch (Exception ex)
		{
			GD.PrintErr("errorCatched!" + ex.ToString());
		}
	}
	private void OnPeerConnected(long id)
	{
		GD.Print("Peer connected with ID: " + id);
	}
}