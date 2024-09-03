using Godot;
using System;

public partial class QueueScene : Node2D
{
	[Export]
	protected AnimationPlayer animationPlayer { get; set; }

	[Export]
	protected Button retunButton { get; set; }

	[Export]
	protected Resource returnTarget { get; set; }

	public override void _Ready()
	{
		animationPlayer.Play("RESET");

		retunButton.Pressed += on_retunButton_pressed;
	}

	private void on_retunButton_pressed()
	{
		ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CloseServer));
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), this.returnTarget.ResourcePath);
	}

}
