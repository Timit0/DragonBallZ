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
	protected Button returnButton {get;set;}

	[Export]
	protected Resource childToAdd {get;set;}


	public override void _Ready()
	{
		returnButton.Pressed += on_return_button_pressed;
		animationPlayer.Play("Zoom");
	}

    private void on_return_button_pressed()
    {
        ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CloseServer));
    }

}
