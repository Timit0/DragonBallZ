using Godot;
using System;

public abstract partial class State : Node
{
    public StateMachine stateMachine;
    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void Ready() { }
    public virtual void Update(float delta) { }
    public virtual void PhysicsUpdate(float delta) { }
    public virtual void HandleInput(InputEvent @event) { }
}