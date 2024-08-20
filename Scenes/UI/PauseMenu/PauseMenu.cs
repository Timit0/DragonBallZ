using Godot;
using System;

public partial class PauseMenu : Control
{
    [ExportGroup("Nodes")]
    [Export]
    protected Button returnButton { get; set; }
    [Export]
    protected Button resumeButton { get; set; }

    [ExportGroup("Targets")]
    [Export]
    protected string returnTarget { get; set; }

    public override void _Ready()
    {
        this.ProcessMode = ProcessModeEnum.Always;
        GetTree().Paused = true;

        returnButton.Pressed += on_return_button_pressed;
        resumeButton.Pressed += on_resume_button_pressed;
        base._Ready();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("inventory_open_or_close"))
        {
            GetTree().Paused = false;
            this.QueueFree();
        }

        base._Input(@event);
    }

    private void on_return_button_pressed()
    {
        SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), returnTarget);
    }

    private void on_resume_button_pressed()
    {
        GetTree().Paused = false;
        this.QueueFree();
    }
}
