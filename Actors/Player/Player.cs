using Godot;
using System;

public partial class Player : Actor
{
	[ExportGroup("Nodes")]
	[Export]
	public AnimationTree AnimationTree {get;set;}
	[Export]
	public Sprite2D Sprite {get;set;}

	[ExportGroup("")]
	[Export]
	public Texture2D TextureOfSprite {get;set;}

    public override void _EnterTree()
    {
		int.TryParse(this.Name, out int id);
		this.SetMultiplayerAuthority(id);
        base._EnterTree();
    }

    public override void _Ready()
	{
		this.Sprite.Texture = TextureOfSprite;	
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		if(IsMultiplayerAuthority())
		{
			Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			this.Velocity = direction * this.Speed;
		}else
		{
			// this.Velocity = //HERE
		}
		this.MoveAndSlide();

		base._PhysicsProcess(delta);
	}

	public void PlayWalk(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Walk", value);
	}

	public void PlayIdle(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Idle", value);
	}

	public void UpdateVelocityAnim()
	{
		this.AnimationTree.Set("parameters/Walk/blend_position", this.Velocity);
		this.AnimationTree.Set("parameters/Idle/blend_position", this.Velocity);
	}
}
