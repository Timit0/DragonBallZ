using Godot;
using System;

public partial class IdleNpcState : NPCState
{
    [Export]
    protected RandomTimer waitingTimer { get; set; }

    public override void Enter()
    {
        waitingTimer.Timeout += on_waiting_timer_timeout;
        waitingTimer.SetUp();
        waitingTimer.Start();

        this.NaviagationAgentSetUp(this.actor.Position, this.actor.Speed);

        this.actor.PlayIdle(true);

        base.Enter();
    }

    public override void Update(float delta)
    {
        if (this.actor.IsMoving)
        {
            this.actor.PlayIdle(false);
            this.actor.PlayWalk(true);
        }
        else
        {
            this.actor.PlayIdle(true);
            this.actor.PlayWalk(false);
        }

        base.Update(delta);
    }

    public override void Exit()
    {
        this.actor.PlayIdle(false);
        this.actor.PlayWalk(false);

        waitingTimer.Timeout -= on_waiting_timer_timeout;
        this.navigationAgent.VelocityComputed -= this.on_velocity_computed;
        base.Exit();
    }

    private void on_waiting_timer_timeout()
    {
        this.stateMachine.TransitionTo("GotToTargetNPCState");
    }
}
