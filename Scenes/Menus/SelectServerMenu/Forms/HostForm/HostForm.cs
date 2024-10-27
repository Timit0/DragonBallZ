using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

public partial class HostForm : Control
{

    [Export]
	protected Button HostButton {get;set;}

	[Export]
	protected Resource HostButtonTarget {get;set;}

	[Export]
	protected string ip {get;set;} = "http://127.0.0.1:8080";

	[Export]
	protected string apiRoute {get;set;} = "/create_host_server";

	protected static readonly System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

	protected FormUrlEncodedContent dataToSend {get;set;}

	public override void _Ready()
	{
		HostButton.Pressed += on_host_button_pressed;
	}

    private void on_host_button_pressed()
    {
		ServerConfigSingleton.Instance.ServerMode = ServerConfigSingleton.ConfigServerEnum.HOST;
		ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CreateServer));
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), HostButtonTarget.ResourcePath);
    }
}
