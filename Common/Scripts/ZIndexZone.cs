using Godot;
using System;

public partial class ZIndexZone : Area2D
{
	[Export]
	protected ZIndex.ZIndexEnum toThisZIndex { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyExited += on_body_exited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (Node node in GetOverlappingBodies())
		{
			if (node is Actor actor)
			{
				actor.ZIndex = (int)toThisZIndex;
			}
		}
	}

	private void on_body_exited(Node2D body)
	{
		if (body is Actor actor)
		{
			actor.ZIndex = (int)global::ZIndex.ZIndexEnum.ACTOR;
		}
	}
}
