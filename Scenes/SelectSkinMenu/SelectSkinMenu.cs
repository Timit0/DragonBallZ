using Godot;
using System;

public partial class SelectSkinMenu : Control
{
	[Export]
	protected AnimationPlayer animationPlayer {get;set;}

	public override void _Ready()
	{
		animationPlayer.Play("Zoom");
	}
}
