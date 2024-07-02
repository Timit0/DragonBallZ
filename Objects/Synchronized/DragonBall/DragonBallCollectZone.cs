using Godot;
using System;

public partial class DragonBallCollectZone : Area2D
{
	public bool ContainPlayer {get;set;}

	protected Player player {get;set;}

    public override void _Process(double delta)
    {
		ContainPlayer = false;
		foreach (Area2D area in GetOverlappingAreas())
		{
			if(area is DetectableZone detectableZone)
			{
				if(detectableZone.Owner is Player player)
				{
					GD.Print("CO");
					this.player = player;
					ContainPlayer = true;
				}
			}
		}
        base._Process(delta);
    }

    public override void _Input(InputEvent @event)
    {
		if(Input.IsActionJustPressed("collect_dragon_ball") && ContainPlayer && player.IsMultiplayerAuthority())
		{
			// Owner.QueueFree();
			Rpc(nameof(this.RemoveDragonBall));
		}

        base._Input(@event);
    }

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    public void RemoveDragonBall()
    {
        Owner.QueueFree();
    }

}
