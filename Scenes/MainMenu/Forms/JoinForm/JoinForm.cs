using Godot;
using System;

public partial class JoinForm : Control
{
	[Export]
	protected LineEdit lineEdit {get;set;}

	[Export]
	protected Button JoinButton {get;set;}
	
	[Export]
	protected Resource JoinButtonTarget {get;set;}

	public override void _Ready()
	{
		JoinButton.Pressed += on_join_button_pressed;
	}

    private void on_join_button_pressed()
    {
		ServerConfigSingleton.Instance.ServerMode = ServerConfigSingleton.ConfigServerEnum.JOIN;
		ServerConfigSingleton.Instance.IpAdresse = lineEdit.Text;
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), JoinButtonTarget.ResourcePath);
    }

}
