using Godot;
using System;

public partial class ZindexSetter : Node2D
{
    [Export]
    protected ZIndex.ZIndexEnum toThisZIndex { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.ZIndex = (int)toThisZIndex;
    }
}
