using Godot;
using System;

public partial class DragonBall : Node2D
{
	[Export]
	public Sprite2D Sprite {get;set;}

    public override void _Ready()
    {
		this.ZIndex = (int)global::ZIndex.ZIndexEnum.DRAGON_BALL;
        base._Ready();
    }
}
