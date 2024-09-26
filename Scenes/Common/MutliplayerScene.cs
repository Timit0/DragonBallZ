using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Godot;
using Godot.Collections;

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

        Array<Vector2> dragonBallsPosition = new Array<Vector2>();

        for (int i = 0; i < 7; i++)
        {
            DragonBall dragonBall = FactorySingleton.Instance.GetThisNodeInstantiateFromString<DragonBall>(dragonBallPath);
            dragonBall.Position = GetRandomPoint(ref dragonBallsPosition);
            dragonBall.Sprite.Frame = i;
            dragonBall.Name = $"DragonBall{i}";
            this.AddChild(dragonBall);
        }
    }

    protected Vector2 GetRandomPoint(ref Array<Vector2> array)
    {
        Random random = new Random();
        Array<Node> listOfPoints = this.DragonBallPointsNode.GetChildren();
        while (true)
        {
            int i = random.Next(0, listOfPoints.Count);
            if (listOfPoints[i] is Node2D point)
            {
                if (!array.Contains(point.Position))
                {
                    array.Add(point.Position);
                    return point.Position;
                }
            }
        }
    }

    protected long GetClientId(int i)
    {
        long.TryParse(ServerAutoload.Instance.PeerInGame[i], out long id);
        return id;
    }
}