using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SelectSkinMenu : Control
{
	[Export]
	protected AnimationPlayer animationPlayer {get;set;}

	[Export]
	protected Godot.Collections.Array<Texture2D> characters {get;set;}

	[Export]
	protected GridContainer gridContainer {get;set;}

	[Export]
	protected Resource childToAdd {get;set;}


	public override void _Ready()
	{
		animationPlayer.Play("Zoom");
	}
}
