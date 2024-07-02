using System;
using System.Collections.Generic;
using Godot;

public partial class Actor : CharacterBody2D
{
    [Export]
    public float Speed {get;set;} = 500;

    [Export]
    public float Tolerance {get;set;} = 1;

    [Export]
    public Node2D FootPoint {get;set;}

    public bool IsMoving 
	{
		get
		{
			if(Math.Abs(this.GetRealVelocity().Y) <= Tolerance && Math.Abs(this.GetRealVelocity().X) <= Tolerance)
			{
				return false;
			}else
			{
				return true;
			}
		}
	}

    public override void _Ready()
    {
		this.ZIndex = (int)global::ZIndex.ZIndexEnum.ACTOR;
        base._Ready();
    }
}