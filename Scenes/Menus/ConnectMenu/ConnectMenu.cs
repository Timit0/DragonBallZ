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
	[Export]
	protected Button optionApiButton { get; set; }
	[Export]
	protected Resource optionApiButtonTarget { get; set; }

	public override void _Ready()
	{
		ApiSingleton.Instance.OverrideIp(SettingsDbContext.Instance.Get().UrlApi);

		optionButton.ItemSelected += on_item_selected;
		formsContainer.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Control>(forms[0]));

		optionApiButton.Pressed += on_option_api_button_pressed;

		animationPlayer.Play("RESET");
	}

	private void on_item_selected(long index)
	{
		RemoveForm();
		int.TryParse(index.ToString(), out int id);
		formsContainer.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Control>(forms[id]));
	}

	private void on_option_api_button_pressed()
	{
		string scenePath = optionApiButtonTarget.ResourcePath;
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePath);
	}

	protected void RemoveForm()
	{
		foreach (Node node in formsContainer.GetChildren())
		{
			formsContainer.RemoveChild(node);
		}
	}
}
