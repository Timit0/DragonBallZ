using Godot;
using System;

public partial class RemapButtonListeningState : BasicRemapStateBase
{
    protected bool _stateIsActive { get; set; } = false;
    public override void Enter()
    {
        this._stateIsActive = true;
        this.buttonToggleAction.Text = "Listening For Input";
        base.Enter();
    }

    public override void Exit()
    {
        this._stateIsActive = false;
        base.Exit();
    }

    public override async void _Input(InputEvent @event)
    {
        if (!_stateIsActive)
        {
            return;
        }

        if (@event is not InputEventMouseMotion && @event is not InputEventMouse)
        {
            InputMap.ActionEraseEvents(this.buttonToggleAction.ActionName);
            InputMap.Singleton.ActionAddEvent(this.buttonToggleAction.ActionName, @event);

            await SettingsDbContext.Instance.UpdateActionEvent(this.buttonToggleAction.ActionName, @event.ToString());

            this.stateMachine.TransitionTo("RemapButtonNormalState");
        }
        base._Input(@event);
    }
}
