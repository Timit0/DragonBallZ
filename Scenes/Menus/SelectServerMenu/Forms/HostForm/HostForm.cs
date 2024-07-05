using Godot;
using System;

public partial class HostForm : Control
{
	[Export]
	protected Button HostButton {get;set;}

	[Export]
	protected Resource HostButtonTarget {get;set;}

	public override void _Ready()
	{
		HostButton.Pressed += on_host_button_pressed;
	}

    private void on_host_button_pressed()
    {
		ServerConfigSingleton.Instance.ServerMode = ServerConfigSingleton.ConfigServerEnum.HOST;
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), HostButtonTarget.ResourcePath);
    }
}
