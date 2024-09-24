using System;
using System.Collections.Generic;
using Godot;

public partial class Actor : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 500;

	[Export]
	public float Tolerance { get; set; } = 1;

	[ExportGroup("Nodes")]
	[Export]
	public AnimationTree AnimationTree { get; set; }

	[Export]
	public Node2D FootPoint { get; set; }

	[Export]
	public Sprite2D Sprite { get; set; }

	public bool IsMoving
	{
		get
		{
			if (Math.Abs(this.Velocity.Y) <= Tolerance && Math.Abs(this.Velocity.X) <= Tolerance)
			{
				return false;
			}
			else
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

	public override void _Process(double delta)
	{
		if (IsMoving)
		{
			UpdateVelocityAnim();
		}

		base._Process(delta);
	}

	public async void ActorSetup()
	{
		// Wait for the first physics frame so the NavigationServer can sync.
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
	}

	public void UpdateVelocityAnim()
	{
		this.AnimationTree.Set("parameters/Walk/blend_position", this.Velocity);
		this.AnimationTree.Set("parameters/Idle/blend_position", this.Velocity);
	}
}