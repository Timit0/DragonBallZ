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

    public bool InGame { get; set; }

    public override void _Ready()
    {
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
            this.PeerInGame.Add(id.ToString());

            this.InGame = true;
            string scenePathToLoad = "res://Scenes/TestScene/TestScene.tscn";
            SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePathToLoad);

            this.PeerQueue.Remove(id.ToString());
        }
    }

    private void on_peer_disconnected(long id)
    {
        GD.Print("PEER DISCONNEC " + id);
        this.PeerQueue.Remove(id.ToString());
    }

    private void on_server_create()
    {
        GD.Print("CREATE SERVER");
        this.Peer.CreateServer(port);
        this.Multiplayer.MultiplayerPeer = this.Peer;
    }

    private void on_client_create()
    {
        GD.Print("CREATE CLIENT IP ENTERED " + ServerConfigSingleton.Instance.IpAdresse);
        this.Peer.CreateClient(ServerConfigSingleton.Instance.IpAdresse, port);
        this.Multiplayer.MultiplayerPeer = this.Peer;
    }

    private void on_server_close()
    {
        GD.Print("SERVER CLOSE");
        this.PeerQueue.Clear();
        this.PeerInGame.Clear();

        this.Multiplayer.MultiplayerPeer = null;
        this.Peer.Close();

        //---------------------------------------
        // this.Multiplayer.MultiplayerPeer.Close();
        // this.Peer = new ENetMultiplayerPeer();

        // this.Peer.PeerConnected += on_peer_connected;
        // this.Peer.PeerDisconnected += on_peer_disconnected;

        // ServerSingals.Instance.CreateServer += on_server_create;
        // ServerSingals.Instance.CreateClient += on_client_create;
        // ServerSingals.Instance.CloseServer += on_server_close;
    }
}
