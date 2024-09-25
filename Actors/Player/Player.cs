using Godot;
using System;

public partial class Player : Actor
{
	[Export]
	public Texture2D TextureOfSprite { get; set; }
	[Export]
	public Resource CameraManagerNodeResource { get; set; }

	// public PlayerModel PlayerModel {get;set;} = new PlayerModel();

	public string TextureOfSpriteString { get; set; }

	public override void _EnterTree()
	{
		int.TryParse(this.Name, out int id);
		this.SetMultiplayerAuthority(id);
		base._EnterTree();
	}

	public override void _Ready()
	{
		ActorSignals.Instance.RemoveActor += on_remove_actor;

		this.TextureOfSpriteString = PlayerSingleton.Instance.PlayerModel.SkinTexture.ResourcePath;
		this.Sprite.Texture = GD.Load<Texture2D>(TextureOfSpriteString);

		if (this.IsMultiplayerAuthority())
		{
			CameraManager cameraManager = FactorySingleton.Instance.GetThisNodeInstantiate<CameraManager>(CameraManagerNodeResource);
			this.AddChild(cameraManager);
		}

		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (IsMultiplayerAuthority())
		{
			Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			this.Velocity = direction * this.Speed;

			PlayerSingleton.Instance.PlayerPostition = this.Position;
		}
		else
		{
			this.Sprite.Texture = GD.Load<Texture2D>(TextureOfSpriteString);
		}

		this.MoveAndSlide();

		base._PhysicsProcess(delta);
	}

	private void on_remove_actor()
	{
		try
		{
			// this.GetChild("CameraNode");
			foreach (Node node in GetChildren())
			{
				if (node is CameraManager cameraManager)
				{
					this.RemoveChild(cameraManager);
				}
			}
			// GetParent().RemoveChild(this);
			this.QueueFree();

		}
		catch (Exception e) { }
	}

	public void PlayWalk(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Walk", value);
	}

	public void PlayIdle(bool value)
	{
		this.AnimationTree.Set($"parameters/conditions/Idle", value);
	}
}
