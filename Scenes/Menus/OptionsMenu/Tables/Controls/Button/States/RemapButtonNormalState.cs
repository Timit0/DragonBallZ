using Godot;
using System;

public partial class RemapButtonNormalState : BasicRemapStateBase
{
    public override void Enter()
    {
        this.buttonToggleAction.Pressed += on_buttonToggleAction_Pressed;
        // this.buttonToggleAction.Text = InputMap.Singleton.ActionGetEvents(this.buttonToggleAction.ActionName)[0].ToString();
        string inputName = InputMap.Singleton.ActionGetEvents(this.buttonToggleAction.ActionName)[0].AsText();
        this.buttonToggleAction.Text = inputName;

        base.Enter();
    }

    public override void Update(float delta)
    {
        base.Update(delta);
    }

    public override void Exit()
    {
        this.buttonToggleAction.Pressed -= on_buttonToggleAction_Pressed;
        base.Exit();
    }

    private void on_buttonToggleAction_Pressed()
    {
        this.stateMachine.TransitionTo("RemapButtonListeningState");
    }
}
