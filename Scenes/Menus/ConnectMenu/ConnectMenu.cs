using Godot;
using Godot.Collections;
using System;

public partial class ConnectMenu : Control
{
	[Export]
	protected OptionButton optionButton { get; set; }
	[Export]
	protected Container formsContainer { get; set; }
	[Export]
	protected Array<Resource> forms { get; set; }
	[Export]
	protected AnimationPlayer animationPlayer { get; set; }

	public override void _Ready()
	{
		optionButton.ItemSelected += on_item_selected;
		formsContainer.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Control>(forms[0]));

		animationPlayer.Play("RESET");
	}


	private void on_item_selected(long index)
	{
		RemoveForm();
		int.TryParse(index.ToString(), out int id);
		formsContainer.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Control>(forms[id]));
	}

	protected void RemoveForm()
	{
		foreach (Node node in formsContainer.GetChildren())
		{
			formsContainer.RemoveChild(node);
		}
	}
}
