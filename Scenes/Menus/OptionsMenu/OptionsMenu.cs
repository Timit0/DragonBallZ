using Godot;
using System;

public partial class OptionsMenu : Control
{
	[Export]
	protected Button returnButton {get;set;}

	// [Export]
	// protected Resource returnTarget {get;set;}


	public override void _Ready()
	{
		returnButton.Pressed += on_return_button_pressed;
	}

    private void on_return_button_pressed()
    {
		// string sceneTarget = returnTarget.ResourcePath;
		string sceneTarget = "res://Scenes/Menus/MainMenu/MainMenu.tscn";
		// GD.Print("Target "+sceneTarget);
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), sceneTarget);
    }

}
