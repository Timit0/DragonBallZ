using Godot;
using System;
using System.Collections.Generic;

public partial class DragonRadar : CanvasLayer
{
	[Export]
	protected float distanceDivider { get; set; } = 1;
	[Export]
	protected Sprite2D DragonBallSpawn { get; set; }
	[Export]
	protected Label ScoreLabel { get; set; }

	public override void _Ready()
	{
		DragonBallSignals.Instance.DragonBallIsRemoved += on_dragon_ball_removed;
		DragonBallSignals.Instance.AddPoint += on_adding_point;

		string dbPath = "res://Scenes/UI/DragonRadar/DragonBall/DragonBallOnRadar.tscn";

		foreach (KeyValuePair<string, DragonBallOnRadarModel> item in DragonBallSingleton.Instance.DragonBallPosition)
		{
			if (!item.Value.Exist)
			{
				continue;
			}
			Control DbOnRadar = FactorySingleton.Instance.GetThisNodeInstantiateFromString<Control>(dbPath);
			DbOnRadar.Position = DbPosition(item.Value.Position);
			DbOnRadar.Name = item.Key;
			DragonBallSpawn.AddChild(DbOnRadar);
		}

		ScoreLabel.Text = GameSingleton.Instance.GetPointsString();
	}

	public override void _Process(double delta)
	{
		foreach (Node item in DragonBallSpawn.GetChildren())
		{
			if (item is DragonBallOnRadar dragonBallOnRadar)
			{
				dragonBallOnRadar.Position = DbPosition(DragonBallSingleton.Instance.DragonBallPosition[dragonBallOnRadar.Name].Position);
			}
		}
		base._Process(delta);
	}

	private void on_dragon_ball_removed(string dBallName)
	{
		foreach (Node item in DragonBallSpawn.GetChildren())
		{
			if (item.Name == dBallName)
			{
				DragonBallSpawn.RemoveChild(item);
			}
		}
	}

	private void on_adding_point()
	{
		ScoreLabel.Text = GameSingleton.Instance.GetPointsString();
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("dragon_radar"))
		{
			DragonBallSignals.Instance.AddPoint -= on_adding_point;
			DragonBallSignals.Instance.DragonBallIsRemoved -= on_dragon_ball_removed;
			this.QueueFree();
		}

		base._Input(@event);
	}

	private Vector2 DbPosition(Vector2 v)
	{
		//coordinate of the middle
		// int middleX = 576;
		// int middleY = 338;

		//Relative coordinate
		float xd = v.X - PlayerSingleton.Instance.PlayerPostition.X;
		float yd = v.Y - PlayerSingleton.Instance.PlayerPostition.Y;

		// return new Vector2(middleX - xd / distanceDivider, middleY - yd / distanceDivider);
		return new Vector2(xd / distanceDivider, yd / distanceDivider);
	}
}
