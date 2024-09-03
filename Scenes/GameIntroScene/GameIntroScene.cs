using Godot;
using System;

public partial class GameIntroScene : Control
{
	//  [Export]
	// public string Scene_to_load = "res://Scenes/Menus/MainMenu/MainMenu.tscn";

	[Export]
	protected Resource resource { get; set; }

	[Export]
	protected AnimationPlayer animationPlayer { get; set; }

	[Export]
	protected Label label { get; set; }



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer.Play("anim");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void gotoScene()
	{
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), resource.ResourcePath);
	}

	public void changeLabelText()
	{
		label.Text = "Ce jeu à était fait par" + '\n' + "Tim Ha et Ogan Özkul";
	}
}
