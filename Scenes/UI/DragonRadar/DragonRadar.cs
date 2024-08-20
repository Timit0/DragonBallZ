using Godot;
using System;
using System.Collections.Generic;

public partial class DragonRadar : CanvasLayer
{
	[Export]
	protected float distanceDivider { get; set; } = 1;
	[Export]
	protected Sprite2D DragonBallSpawn { get; set; }

	public List<Vector2> DragonBallPosition { get; set; }
	public Vector2 PlayerPosition { get; set; }

	public override void _Ready()
	{
		string dbPath = "res://Scenes/UI/DragonRadar/DragonBall/DragonBallOnRadar.tscn";
		foreach (Vector2 item in DragonBallPosition)
		{
			Control DbOnRadar = FactorySingleton.Instance.GetThisNodeInstantiateFromString<Control>(dbPath);
			DbOnRadar.Position = DbPosition(item);
			DragonBallSpawn.AddChild(DbOnRadar);
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("dragon_radar"))
		{
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
		float xd = v.X - PlayerPosition.X;
		float yd = v.Y - PlayerPosition.Y;

		// return new Vector2(middleX - xd / distanceDivider, middleY - yd / distanceDivider);
		return new Vector2(xd / distanceDivider, yd / distanceDivider);
	}

	public void SetUp(List<Vector2> DragonBallPosition, Vector2 PlayerPosition)
	{
		this.DragonBallPosition = DragonBallPosition;
		this.PlayerPosition = PlayerPosition;
	}
}
