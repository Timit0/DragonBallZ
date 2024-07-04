using Godot;
using System;

public partial class MainMenu : Control
{
	[ExportGroup("Nodes")]
	[Export]
	protected AnimationPlayer shenronAnimationPlayer {get;set;}
	[Export]
	protected AnimationPlayer cameraAnimationPlayer {get;set;}

	[Export]
	protected Button startButton {get;set;}
	[Export]
	protected Button optionsButton {get;set;}
	[Export]
	protected Button quitButton {get;set;}


	[ExportGroup("Targets")]
	[Export]
	protected Resource startTarget {get;set;}
	[Export]
	protected Resource optionsTarget {get;set;}

	public override void _Ready()
	{
		shenronAnimationPlayer.Play("RESET");
		cameraAnimationPlayer.Play("RESET");

		startButton.Pressed += on_start_button_pressed;
		optionsButton.Pressed += on_options_button_pressed;
		quitButton.Pressed += on_quit_button_pressed;
	}

    private void on_start_button_pressed()
    {
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), startTarget.ResourcePath);
    }


    private void on_options_button_pressed()
    {
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), optionsTarget.ResourcePath);
    }


    private void on_quit_button_pressed()
    {
        GetTree().Quit();
    }

}
