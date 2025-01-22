using Godot;
using System;

public partial class WaitToBeJoinScene : Node2D
{
	[Export]
	protected AnimationPlayer animationPlayer { get; set; }
	// [Export]
	// protected Resource returnTarget { get; set; }
	[Export]
	protected Button returnButton { get; set; }

	public override void _Ready()
	{
		animationPlayer.Play("RESET");
		returnButton.Pressed += on_return_button_pressed;
		base._Ready();
	}

	private void on_return_button_pressed()
	{
		string scenePath = "res://Scenes/Menus/SelectServerMenu/SelectServerMenu.tscn";
		ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CloseServer));
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePath);
	}

}
