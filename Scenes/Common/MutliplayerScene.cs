using System;
using Godot;

public partial class MultiplayerScene : Scene
{
    public ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
    protected int port {get;set;} = 1555;

    public override void _Ready()
    {
        if(ServerConfigSingleton.Instance.ServerMode == ServerConfigSingleton.ConfigServerEnum.HOST)
        {
            Peer.CreateServer(port);
            this.Multiplayer.MultiplayerPeer = Peer;
            this.Multiplayer.PeerConnected += on_peer_connected;
            this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId(1));
        }

        if(ServerConfigSingleton.Instance.ServerMode == ServerConfigSingleton.ConfigServerEnum.JOIN)
        {
            GD.Print(ServerConfigSingleton.Instance.IpAdresse);
            Peer.CreateClient(ServerConfigSingleton.Instance.IpAdresse, port);
            this.Multiplayer.MultiplayerPeer = Peer;
        }

        base._Ready();
    }

    private void on_peer_connected(long id)
    {
        int.TryParse(id.ToString(), out int idOut);
        this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId(idOut));
    }
}