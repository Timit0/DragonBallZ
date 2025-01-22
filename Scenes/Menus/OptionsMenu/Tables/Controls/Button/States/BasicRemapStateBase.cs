using Godot;
using System;

public partial class BasicRemapStateBase : State
{
    protected ButtonToggleAction buttonToggleAction
    {
        get => this.Owner as ButtonToggleAction;
        set => this.buttonToggleAction = value;
    }
}
