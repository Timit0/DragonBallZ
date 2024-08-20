using Godot;
using System;

public partial class DragonBallOnRadar : Control
{
	[Export]
	protected AnimationPlayer animationPlayer { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer.Play("RESET");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
