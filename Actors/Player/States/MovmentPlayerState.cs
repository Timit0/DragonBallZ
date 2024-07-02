using Godot;
using System;

public partial class MovmentPlayerState : PlayerState
{
	public override void Enter()
    {
		this.player.PlayWalk(true);
        base.Enter();
    }

    public override void Update(float delta)
    {
		if(!this.player.IsMoving)
		{
			this.stateMachine.TransitionTo("IdlePlayerState");
		}else
        {
            this.player.UpdateVelocityAnim();
        }

        base.Update(delta);
    }

    public override void Exit()
    {
		this.player.PlayWalk(false);
        base.Exit();
    }
}
