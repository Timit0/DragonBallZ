using Godot;
using System;

public partial class QueueScene : Node2D
{
	[Export]
	protected AnimationPlayer loadingRectAnimationPlayer { get; set; }

	[Export]
	protected AnimationPlayer cameraAnimationPlayer { get; set; }

	[Export]
	protected Button retunButton { get; set; }

	[Export]
	protected Resource returnTarget { get; set; }

	public bool HasAlready2Player { get; set; }

	public override void _Ready()
	{
		loadingRectAnimationPlayer.Play("RESET");
		cameraAnimationPlayer.Play("RESET");

		retunButton.Pressed += on_retunButton_pressed;

		ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.PlayerReadyState), 1);
	}

	private void on_retunButton_pressed()
	{
		// ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.CloseServer));
		ServerSingals.Instance.EmitSignal(nameof(ServerSingals.Instance.PlayerReadyState), -1);
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), this.returnTarget.ResourcePath);
	}

}
