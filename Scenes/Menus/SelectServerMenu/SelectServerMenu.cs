using Godot;
using Godot.Collections;
using System;

public partial class SelectServerMenu : Control
{
	[ExportGroup("Nodes")]
	[Export]
	protected Container formsContainer {get;set;}
	[Export]
	public OptionButton OptionButton {get;set;}
	[Export]
	protected Array<Resource> forms {get;set;}
	[Export]
	protected AnimationPlayer animationPlayer {get;set;}

	public override void _Ready()
	{
		animationPlayer.Play("RESET");
		OptionButton.ItemSelected += on_item_selected;
		formsContainer.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Control>(forms[0]));
	}


    private void on_item_selected(long index)
    {
        RemoveForm();
		int.TryParse(index.ToString(), out int id);
		formsContainer.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Control>(forms[id]));
    }


	private void on_return_button_pressed()
    {
		string sceneTarget = "res://Scenes/Menus/MainMenu/MainMenu.tscn";
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), sceneTarget);
    }

	protected void RemoveForm()
	{
		foreach (Node node in formsContainer.GetChildren())
		{
			formsContainer.RemoveChild(node);
		}
	}
}
