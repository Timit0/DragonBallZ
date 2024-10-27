using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

public partial class ServerAutoload : Node
{

    public static ServerAutoload Instance { get; private set; }

    public ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
    protected int port { get; set; } = 1555;
    protected int maxPeer { get; set; } = 1;

    public List<string> PeerQueue { get; set; } = new List<string>();
    public List<string> PeerInGame { get; set; } = new List<string>();

    protected int numberOfPlayerReady {get;set;} = 0;

    public override void _Ready()
    {
        this.ProcessMode = ProcessModeEnum.Always;
        Instance = this;

        this.Peer.PeerConnected += on_peer_connected;
        this.Peer.PeerDisconnected += on_peer_disconnected;

        ServerSingals.Instance.CreateServer += on_server_create;
        ServerSingals.Instance.CreateClient += on_client_create;
        ServerSingals.Instance.CloseServer += on_server_close;

        ServerSingals.Instance.PlayerReadyState += on_player_ready_state;
    }

    public override void _Process(double delta)
    {
        // GD.Print(this.numberOfPlayerReady);
        base._Process(delta);
    }

    private void on_peer_connected(long id)
    {
        GD.Print("PEER ADDED " + id);
        this.PeerQueue.Add(id.ToString());

        if (this.PeerQueue.Count == 1)
        {
            GD.Print("START GAME");
            this.PeerInGame.Add(id.ToString());
        }
    }

    private void on_peer_disconnected(long id)
    {
        GD.Print("PEER DISCONNEC " + id);
        this.PeerQueue.Remove(id.ToString());
        this.PeerInGame.Remove(id.ToString());

        // if (id == 1)
        // {
        string scenePathToLoad = "res://Scenes/Menus/MainMenu/MainMenu.tscn";
        ActorSignals.Instance.EmitSignal(nameof(ActorSignals.Instance.RemoveActor));
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePathToLoad);
        this.CloseServer();
        // }
    }

    private async void on_server_create()
    {
        this.numberOfPlayerReady = 0;
        this.PeerQueue.Clear();
        this.PeerInGame.Clear();

        GD.Print("CREATE SERVER");
        this.Peer.CreateServer(port, maxPeer);
        this.Multiplayer.MultiplayerPeer = this.Peer;

        await CreateHostServer();
    }

    private void on_client_create()
    {
        this.PeerQueue.Clear();
        this.PeerInGame.Clear();

        GD.Print("CREATE CLIENT IP ENTERED " + ServerConfigSingleton.Instance.IpAdresse);
        this.Peer.CreateClient(ServerConfigSingleton.Instance.IpAdresse, port);
        this.Multiplayer.MultiplayerPeer = this.Peer;
    }

    private void on_server_close()
    {
        CloseServer();
    }

    private void on_player_ready_state(int i)
    {
        Rpc(nameof(this.PlayerReadyIncrement), i);
        if(this.numberOfPlayerReady == 2)
        {
            Rpc(nameof(this.StartGame));
        }
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    public void StartGame()
    {
        string scenePathToLoad = "res://Scenes/level/levelScene.tscn";
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePathToLoad);

            // this.PeerQueue.Remove(id.ToString());
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    protected void PlayerReadyIncrement(int i)
    {
        this.numberOfPlayerReady+=i;
    }

    public async void CloseServer()
    {
        await DeleteHostServer();

        GD.Print("SERVER CLOSE");
        this.PeerQueue.Clear();
        this.PeerInGame.Clear();

        this.Multiplayer.MultiplayerPeer = null;
        this.Peer.Close();
    }

    public async Task<bool> CreateHostServer()
	{
        this.numberOfPlayerReady = 0;
		FormUrlEncodedContent dataToSend = new FormUrlEncodedContent(
			new[]
			{
				new KeyValuePair<string, string>("host_username", UserSingleton.Instance.User.Username),
				new KeyValuePair<string, string>("host_ip", GetLocalIPAddress()),
			}
		);

		return await ApiSingleton.Instance.PostOnApiWithNotification("/create_host_server", dataToSend);
	}

    public async Task<bool> DeleteHostServer()
    {
        this.numberOfPlayerReady = 0;
        FormUrlEncodedContent dataToSend = new FormUrlEncodedContent(
			new[]
			{
				new KeyValuePair<string, string>("username", UserSingleton.Instance.User.Username),
			}
		);

		return await ApiSingleton.Instance.PostOnApiWithNotification("/delete_host_server", dataToSend);
    }

    public string GetLocalIPAddress()
	{
		foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				return ip.ToString();
			}
		}
		throw new Exception("No IPv4 address found for the local machine.");
	}
}
