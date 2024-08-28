using Godot;
using System;

public partial class DetectableZone : Area2D
{
	public override void _Process(double delta)
	{
		try
		{
			foreach (Node node in GetOverlappingAreas())
			{
				if (node is DetectableZone)
				{
					DetectableZone area = node as DetectableZone;
					EmitSignalWhenActorIsBehind(area);
				}
			}
		}
		catch (Exception e) { }
	}

	private void EmitSignalWhenActorIsBehind(Area2D area)
	{
		try
		{
			if (area.Owner is Actor)
			{
				Actor actor = area.Owner as Actor;
				if (actor.FootPoint.GlobalPosition.Y < ((Actor)this.Owner).FootPoint.GlobalPosition.Y)
				{
					ActorSignals.Instance.EmitSignal(nameof(ActorSignals.Instance.BehindActor), this.Owner.Name, area.Owner.Name);
				}
			}
		}
		catch (Exception e) { }
	}
}
