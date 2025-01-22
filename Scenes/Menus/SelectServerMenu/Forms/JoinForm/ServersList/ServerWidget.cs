using Godot;
using System;
using System.Collections.Generic;
using System.Net.Http;

public partial class ServerWidget : Control
{
    [Export]
    public Label Label {get;set;}
    [Export]
    protected Button button {get;set;}
    [Export]
    protected Resource joinButtonTarget {get;set;}

    public HostServer HostServer {get;set;} = new HostServer();

    public override void _Ready()
    {
        button.Pressed += on_button_join_pressed;

        this.Label.Text = this.HostServer.UserHost.Username;
        base._Ready();
    }

    private async void on_button_join_pressed()
    {
        FormUrlEncodedContent dataToSend = new FormUrlEncodedContent(
			new[]
			{
				new KeyValuePair<string, string>("username", this.HostServer.UserHost.Username),
			}
		);
        bool canAddGuestToHostServer = await ApiSingleton.Instance.PostOnApiWithNotification("/can_add_guest_to_host_server", dataToSend);

        if(!canAddGuestToHostServer)
        {
            return;
        }

        FormUrlEncodedContent dataToSend2 = new FormUrlEncodedContent(
			new[]
			{
				new KeyValuePair<string, string>("host_username", this.HostServer.UserHost.Username),
				new KeyValuePair<string, string>("guest_username", UserSingleton.Instance.User.Username),
			}
		);

        await ApiSingleton.Instance.PostOnApiWithNotification("/add_player_guest_to_host_server", dataToSend2);

        ServerConfigSingleton.Instance.ServerMode = ServerConfigSingleton.ConfigServerEnum.JOIN;
		ServerConfigSingleton.Instance.IpAdresse = this.HostServer.HostIp;
        ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CreateClient));
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), joinButtonTarget.ResourcePath);
    }

}
