using Godot;
using System;

public partial class ReturnButton : Button
{

	[ExportGroup("Target")]
	[Export]
	protected Resource targetResource {get;set;}
	[Export]
	protected string targetPath {get;set;} = "";

	public override void _Ready()
	{
		this.Pressed += on_button_pressed;
	}

    private void on_button_pressed()
    {
        if(targetPath != string.Empty)
		{
			SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), targetPath);
			return;
		}

		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), targetResource.ResourcePath);
    }

}
