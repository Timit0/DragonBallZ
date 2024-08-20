using Godot;
using System;

public partial class DragonBallCollectZone : Area2D
{
	public bool ContainPlayer { get; set; }

	protected Player player { get; set; }

	public override void _Process(double delta)
	{
		ContainPlayer = false;
		foreach (Area2D area in GetOverlappingAreas())
		{
			if (area is DetectableZone detectableZone)
			{
				if (detectableZone.Owner is Player player)
				{
					this.player = player;
					ContainPlayer = true;
				}
			}
		}
		base._Process(delta);
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("collect_dragon_ball") && ContainPlayer && player.IsMultiplayerAuthority())
		{
			Rpc(nameof(this.SetDragonBallToExistInTheDic));
			Rpc(nameof(this.AddPoint));
			Rpc(nameof(this.RemoveDragonBall));
		}

		base._Input(@event);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void RemoveDragonBall()
	{
		Owner.QueueFree();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void SetDragonBallToExistInTheDic()
	{
		DragonBallSingleton.Instance.DragonBallPosition[this.Owner.Name].Exist = false;
		DragonBallSignals.Instance.EmitSignal(nameof(DragonBallSignals.Instance.DragonBallIsRemoved), this.Owner.Name);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void AddPoint()
	{
		GameSingleton.Instance.Points++;
		DragonBallSignals.Instance.EmitSignal(nameof(DragonBallSignals.Instance.AddPoint));
	}
}
