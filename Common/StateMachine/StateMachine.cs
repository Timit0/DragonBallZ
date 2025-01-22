using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    [Export]
    public NodePath initialState { get; set; }

    private Dictionary<string, State> _states;
    protected State _currentState;

    public override void _Ready()
    {
        _states = new Dictionary<string, State>();
        foreach (Node node in GetChildren())
        {
            if (node is State s)
            {
                _states[node.Name] = s;
                s.stateMachine = this;
                // s.Ready();
                // s.Exit();
            }
        }
        _currentState = GetNode<State>(initialState);
        _currentState.Enter();

        base._Ready();
    }

    public override void _Process(double delta)
    {
        _currentState.Update((float)delta);
        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentState.PhysicsUpdate((float)delta);
        base._PhysicsProcess(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState.HandleInput(@event);
        base._UnhandledInput(@event);
    }

    public void TransitionTo(string key)
    {
        if (!_states.ContainsKey(key) || _currentState == _states[key])
        {
            return;
        }

        _currentState.Exit();
        _currentState = _states[key];
        _currentState.Enter();
    }

    public string GetStateName()
    {
        return _currentState.Name;
    }
}