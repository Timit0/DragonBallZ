using Godot;
using System;
using Godot.Collections;

public partial class GotToTargetNpcState : NPCState
{
    Vector2 actualTarget { get; set; } = new Vector2();

    public override void Enter()
    {
        Callable.From(this.actor.ActorSetup).CallDeferred();
        NaviagationAgentSetUp(GetNewTarget(), this.actor.Speed);
        this.actor.PlayWalk(true);

        base.Enter();
    }

    protected override void on_velocity_computed(Vector2 safeVelocity)
    {
        base.on_velocity_computed(safeVelocity);
    }


    public override void Update(float delta)
    {
        // this.actor.UpdateVelocityAnim();
        this.UpdateNavAgent();
        base.Update(delta);
    }

    public override void Exit()
    {
        this.actor.PlayWalk(false);
        this.navigationAgent.TargetReached -= on_target_reached;
        this.navigationAgent.VelocityComputed -= on_velocity_computed;
        base.Exit();
    }

    private void on_target_reached()
    {
        this.stateMachine.TransitionTo("IdleNPCState");
    }

    protected override void NaviagationAgentSetUp(Vector2 target, float speed)
    {
        this.navigationAgent.TargetReached += on_target_reached;
        base.NaviagationAgentSetUp(target, speed);
    }

    protected Vector2 GetNewTarget()
    {
        Array<Node> list = GetScene().TargetPointsNode.GetChildren();

        Random random = new Random();

        for (int i = 0; i < 20; i++)
        {
            int nodeI = random.Next(0, list.Count);
            if (GetScene().TargetPointsNode.GetChild(nodeI) is Node2D targetPoint)
            {
                if (targetPoint.Position != this.actualTarget)
                {
                    actualTarget = targetPoint.Position;
                    return targetPoint.Position;
                }
            }
        }
        return this.actor.Position;
    }

    protected Scene GetScene()
    {
        return Owner.GetParent() as Scene;
    }
}
