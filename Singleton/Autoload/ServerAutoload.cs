using Godot;
using System;
using System.Collections.Generic;

public partial class ServerAutoload : Node
{

    public static ServerAutoload Instance { get; private set; }

    public ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
    protected int port { get; set; } = 1555;

    public List<string> PeerQueue { get; set; } = new List<string>();
    public List<string> PeerInGame { get; set; } = new List<string>();

    public override void _Ready()
    {
        this.ProcessMode = ProcessModeEnum.Always;
        Instance = this;

        this.Peer.PeerConnected += on_peer_connected;
        this.Peer.PeerDisconnected += on_peer_disconnected;

        ServerSingals.Instance.CreateServer += on_server_create;
        ServerSingals.Instance.CreateClient += on_client_create;
        ServerSingals.Instance.CloseServer += on_server_close;
    }

    private void on_peer_connected(long id)
    {
        GD.Print("PEER ADDED " + id);
        this.PeerQueue.Add(id.ToString());

        if (this.PeerQueue.Count == 1)
        {
            GD.Print("START GAME");
            this.PeerInGame.Add(id.ToString());

            string scenePathToLoad = "res://Scenes/level/levelScene.tscn";
            SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePathToLoad);

            this.PeerQueue.Remove(id.ToString());
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

    private void on_server_create()
    {
        this.PeerQueue.Clear();
        this.PeerInGame.Clear();

        GD.Print("CREATE SERVER");
        this.Peer.CreateServer(port);
        this.Multiplayer.MultiplayerPeer = this.Peer;
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

    public void CloseServer()
    {
        GD.Print("SERVER CLOSE");
        this.PeerQueue.Clear();
        this.PeerInGame.Clear();

        this.Multiplayer.MultiplayerPeer = null;
        this.Peer.Close();
    }
}
