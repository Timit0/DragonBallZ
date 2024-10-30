using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class WinningScene : Scene
{
	[Export]
	protected AnimationPlayer animationPlayer { get; set; }

	[Export]
	protected Label hostLabel { get; set; }
	[Export]
	protected Label guestLabel { get; set; }

	// Called when the node enters the scene tree for the first time.
	public async override void _Ready()
	{
		animationPlayer.Play("RESET");

		List<HostServer> hostServers = await ApiSingleton.Instance.PostOnApiGetAllHostServer();
		HostServer hostServer = new HostServer();

		if (this.Multiplayer.MultiplayerPeer != null && this.IsMultiplayerAuthority())
		{
			// UserSingleton.Instance.User.Username = "timit0";
			hostServer = hostServers.Where(x => x.UserHost.Username == UserSingleton.Instance.User.Username).First();
		}
		else
		{
			// UserSingleton.Instance.User.Username = "timit1";
			hostServer = hostServers.Where(x => x.UserGuest.Username == UserSingleton.Instance.User.Username).First();
		}

		GD.Print(hostServer.HostIp);

		this.hostLabel.Text = hostServer.UserHost.Username;
		this.guestLabel.Text = hostServer.UserGuest.Username;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
