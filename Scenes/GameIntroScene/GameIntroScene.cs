using Godot;
using System;

public partial class GameIntroScene : Control
{
	[Export]
	protected Resource resource { get; set; }

	[Export]
	protected AnimationPlayer animationPlayer { get; set; }

	[Export]
	protected Label label { get; set; }

	public override void _Ready()
	{
		animationPlayer.Play("anim");
	}

    public override void _Input(InputEvent @event)
    {
		if(Input.IsActionJustPressed("skip"))
		{
			SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), resource.ResourcePath);
		}
        base._Input(@event);
    }

    public void GotoScene()
	{
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), resource.ResourcePath);
	}
}
