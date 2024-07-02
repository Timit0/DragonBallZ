using Godot;
using System;

public partial class IdlePlayerState : PlayerState
{
    public override void Enter()
    {
		this.player.PlayIdle(true);
        base.Enter();
    }

    public override void Update(float delta)
    {
		if(this.player.IsMoving)
		{
			this.stateMachine.TransitionTo("MovmentPlayerState");
		}
        base.Update(delta);
    }

    public override void Exit()
    {
		this.player.PlayIdle(false);
        base.Exit();
    }
}
