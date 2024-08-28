using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Godot;

public partial class MultiplayerScene : Scene
{
    // public ENetMultiplayerPeer Peer = new ENetMultiplayerPeer();
    // protected int port { get; set; } = 1555;

    public override void _Ready()
    {
        SceneSignals.Instance.ChangeToThisScene += on_quit_this_scene;

        // if (ServerConfigSingleton.Instance.ServerMode == ServerConfigSingleton.ConfigServerEnum.HOST)
        if (this.IsMultiplayerAuthority())
        {
            // Peer.CreateServer(port);
            // this.Multiplayer.MultiplayerPeer = Peer;
            // this.Multiplayer.PeerConnected += on_peer_connected;
            this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId((int)this.GetClientId(0)));
            this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId(1));

            CreateDragonBalls();
        }

        if (ServerConfigSingleton.Instance.ServerMode == ServerConfigSingleton.ConfigServerEnum.JOIN)
        {
            // Peer.CreateClient(ServerConfigSingleton.Instance.IpAdresse, port);
            // this.Multiplayer.MultiplayerPeer = Peer;
            // GD.Print((int)this.GetClientId(0));
            // this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId((int)this.GetClientId(0)));
            // this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId(1));
        }

        base._Ready();
    }

    private void on_quit_this_scene(string scenePath)
    {
        // ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CloseServer));
        // Peer.Close();
        // this.Peer = new ENetMultiplayerPeer();
    }


    // private void on_peer_connected(long id)
    // {
    //     int.TryParse(id.ToString(), out int idOut);
    //     this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId(idOut));
    // }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("dragon_radar"))
        {
            foreach (Node node in GetChildren())
            {
                if (node is DragonRadar)
                {
                    return;
                }
            }
            this.AddChild(FactorySingleton.Instance.GetDragonRadar());
        }

        base._Input(@event);
    }

    protected virtual void CreateDragonBalls(int dBNumb = 7)
    {
        string dragonBallPath = "res://Objects/Synchronized/DragonBall/DragonBall.tscn";

        Random random = new Random();

        for (int i = 0; i < 7; i++)
        {
            DragonBall dragonBall = FactorySingleton.Instance.GetThisNodeInstantiateFromString<DragonBall>(dragonBallPath);
            dragonBall.Position = new Vector2(random.Next(0, 10000), random.Next(0, 10000));
            dragonBall.Sprite.Frame = random.Next(0, 7);
            dragonBall.Name = $"DragonBall{i}";
            this.AddChild(dragonBall);
        }
    }

    protected long GetClientId(int i)
    {
        long.TryParse(ServerAutoload.Instance.PeerInGame[i], out long id);
        return id;
    }
}