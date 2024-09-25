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

	//-------------------------------------------------------------------
	[Export]
	protected Label stateLabel { get; set; }
	//-------------------------------------------------------------------

	public string TextureOfSpriteString { get; set; }

	// public override void _EnterTree()
	// {
	// 	int.TryParse(this.Name, out int id);
	// 	this.SetMultiplayerAuthority(id);
	// 	base._EnterTree();
	// }

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
		// ActorSignals.Instance.RemoveActor += on_remove_actor;

		// this.TextureOfSpriteString = PlayerSingleton.Instance.PlayerModel.SkinTexture.ResourcePath;
		// this.Sprite.Texture = GD.Load<Texture2D>(TextureOfSpriteString);

		base._Ready();
	}

	public override void _Process(double delta)
	{
		if (this.IsMultiplayerAuthority())
		{
			this.stateLabel.Text = "State : " + this.stateMachine.GetStateName();
		}
		base._Process(delta);
	}

	// public override void _PhysicsProcess(double delta)
	// {
	// 	this.MoveAndSlide();

	// 	base._PhysicsProcess(delta);
	// }

	// private void on_remove_actor()
	// {
	// 	try
	// 	{
	// 		GetParent().RemoveChild(this);
	// 	}
	// 	catch (Exception e) { }
	// }

	// public override void _PhysicsProcess(double delta)
	// {
	// 	GD.Print(Velocity);
	// 	base._PhysicsProcess(delta);
	// }

	public void PlayWalk(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Walk", value);
	}

	public void PlayIdle(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Idle", value);
	}
}
