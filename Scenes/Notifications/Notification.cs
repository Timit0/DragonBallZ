using Godot;
using System;

public partial class Notification : Node2D
{
	[Export]
	AnimationPlayer animationPlayer { get; set; }
	[Export]
	Label label { get; set; }

	public string Content { get; set; }

	public override void _Ready()
	{
		this.label.Text = Content;
		animationPlayer.Play("RESET");
	}
}
