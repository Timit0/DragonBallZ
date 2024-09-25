using Godot;
using System;

public partial class Npc : Actor
{
	// [Export]
	// public Camera2D Camera { get; set; }
	[ExportGroup("NodeToDeleteIfNotAuth")]
	[Export]
	protected NavigationAgent2D navigationAgent2D { get; set; }

	[Export]
	protected StateMachine stateMachine { get; set; }


	[ExportGroup("")]
	[Export]
	public Texture2D TextureOfSprite { get; set; }
	[Export]
	protected int minSpeed { get; set; }
	[Export]
	protected int maxSpeed { get; set; }
	//-------------------------------------------------------------------
	[Export]
	protected Label stateLabel { get; set; }
	//-------------------------------------------------------------------

	public string TextureOfSpriteString { get; set; }

	public float Speed { get; set; }

	public override void _EnterTree()
	{
		if (!this.IsMultiplayerAuthority())
		{
			navigationAgent2D.QueueFree();
			// stateMachine.QueueFree();
			foreach (Node node in stateMachine.GetChildren())
			{
				stateMachine.RemoveChild(node);
			}
			this.RemoveChild(stateMachine);
		}
		base._EnterTree();
	}

	public override void _Ready()
	{
		ActorSignals.Instance.RemoveActor += on_remove_actor;

		// this.TextureOfSpriteString = PlayerSingleton.Instance.PlayerModel.SkinTexture.ResourcePath;
		// this.Sprite.Texture = GD.Load<Texture2D>(TextureOfSpriteString);

		base._Ready();
	}

	public override void _Process(double delta)
	{
		// if (this.IsMultiplayerAuthority())
		// {
		// 	this.stateLabel.Text = "State : " + this.stateMachine.GetStateName();
		// }
		base._Process(delta);
	}

	private void on_remove_actor()
	{
		// GetParent().RemoveChild(this);
		this.QueueFree();
	}

	public void PlayWalk(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Walk", value);
	}

	public void PlayIdle(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Idle", value);
	}

	public void NewSpeed()
	{
		Random random = new Random();
		Speed = random.Next(minSpeed, maxSpeed + 1);
	}
}
