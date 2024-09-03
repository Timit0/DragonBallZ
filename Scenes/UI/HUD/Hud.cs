using Godot;
using System;
using System.Collections.Generic;

public partial class Hud : CanvasLayer
{
	[Export]
	protected Label nameLabel { get; set; }

	[Export]
	protected Label dragonballCounterLabel { get; set; }

	[Export]
	protected Sprite2D avatar { get; set; }

	const string GOKU = "gokuSkin";
	const string BULMA = "bulmaSkin";
	const string VEGETA = "VegetaSkin";
	const string KRILIN = "krilinSkin";

	Dictionary<string, string> nameDic = new Dictionary<string, string>() {
		{ GOKU, "Goku" },
		{ BULMA, "Bulma" },
		{ VEGETA, "Vegeta" },
		{ KRILIN, "Krilin" },
		{ "", "Hello World!" }
	};

	Dictionary<string, float> PhotoYcoordsDic = new Dictionary<string, float>()
	{
		{ GOKU, 0 },
		{ BULMA, 128 },
		{ VEGETA, 64 },
		{ KRILIN, 192 },
		{ "", 0 }
	};

	public override void _Ready()
	{
		TrySetAvatarAndName(GOKU);
		TrySetAvatarAndName(BULMA);
		TrySetAvatarAndName(VEGETA);
		TrySetAvatarAndName(KRILIN);

		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		dragonballCounterLabel.Text = GameSingleton.Instance.GetPointsString();
	}

	protected void TrySetAvatarAndName(string avatarName)
	{
		if (PlayerSingleton.Instance.PlayerModel.SkinTexture.ResourcePath.ToUpper().Contains(avatarName.ToUpper()))
		{
			avatar.SetRegionRect(new Rect2(
				new Vector2(0, PhotoYcoordsDic[avatarName]),
				new Vector2(64, 64)
			));
			this.nameLabel.Text = nameDic[avatarName];
		}
	}
}
