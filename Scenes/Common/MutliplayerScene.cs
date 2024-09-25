using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Godot;

public partial class MultiplayerScene : Scene
{
    public override void _Ready()
    {
        if (this.IsMultiplayerAuthority())
        {
            this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId((int)this.GetClientId(0)));
            this.AddChild(FactorySingleton.Instance.AddPlayerWithThisId(1));

            CreateDragonBalls();
        }

        base._Ready();
    }


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
            dragonBall.Position = new Vector2(random.Next(-10000, 10000), random.Next(-10000, 10000));
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