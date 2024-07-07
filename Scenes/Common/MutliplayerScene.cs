using System;
using System.ComponentModel.Design;
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

            CreateDragonBalls();
        }

        if(ServerConfigSingleton.Instance.ServerMode == ServerConfigSingleton.ConfigServerEnum.JOIN)
        {
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

    protected virtual void CreateDragonBalls(int dBNumb = 7)
    {
        string dragonBallPath = "res://Objects/Synchronized/DragonBall/DragonBall.tscn";

        Random random = new Random();

        for (int i = 0; i < 7; i++)
        {
		    DragonBall dragonBall = FactorySingleton.Instance.GetThisNodeInstantiateFromString<DragonBall>(dragonBallPath);
            dragonBall.Position = new Vector2(random.Next(0, 1000), random.Next(0, 500));
            dragonBall.Sprite.Frame = random.Next(0, 7);
            dragonBall.Name = $"DragonBall{i}";
            this.AddChild(dragonBall);
        }
    }
}