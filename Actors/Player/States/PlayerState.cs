using Godot;
using System;

public abstract partial class PlayerState : State
{
    protected Player player 
    {
        get => this.Owner as Player;
        set => this.player = value;
    }
}