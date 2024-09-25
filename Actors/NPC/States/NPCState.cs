using Godot;
using System;

public partial class NPCState : State
{
    [Export]
    protected NavigationAgent2D navigationAgent { get; set; }

    protected Npc actor
    {
        get => this.Owner as Npc;
        set => this.actor = value;
    }

    protected void UpdateNavAgent()
    {
        Vector2 currentAgentPosition = this.actor.GlobalTransform.Origin;
        Vector2 nextPathPosition = navigationAgent.GetNextPathPosition();

        navigationAgent.Velocity = currentAgentPosition.DirectionTo(nextPathPosition) * this.actor.Speed;
    }

    protected virtual void NaviagationAgentSetUp(Vector2 target)
    {
        this.actor.NewSpeed();
        this.navigationAgent.VelocityComputed += on_velocity_computed;
        this.navigationAgent.TargetPosition = target;
        this.navigationAgent.MaxSpeed = actor.Speed;

        // this.navigationAgent.TargetReached += on_target_reached;
    }

    protected virtual void on_velocity_computed(Vector2 safeVelocity)
    {
        this.actor.Velocity = safeVelocity;
        this.actor.MoveAndSlide();
    }
}