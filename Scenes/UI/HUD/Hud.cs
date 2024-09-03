using Godot;
using System;
using System.Collections.Generic;

public partial class Hud : CanvasLayer
{
	[Export]
	Label nameLabel;

	[Export]
	Label dragonballCounterLabel;

	[Export]
	Sprite2D avatar;

	bool isReady = false;

	string tempStr = "";

	const string GOKU_SKIN_FILENAME = "gokuSkin.png";
	const string BULMA_SKIN_FILENAME = "bulmaSkin.png";
	const string VEGETA_SKIN_FILENAME = "VegetaSkin.png";
	const string KRILIN_SKIN_FILENAME = "krilinSkin.png";

	Dictionary<string, string> guiName = new Dictionary<string, string>() {
		{ GOKU_SKIN_FILENAME, "Goku" },
		{ BULMA_SKIN_FILENAME, "Bulma" },
		{ VEGETA_SKIN_FILENAME, "Vegeta" },
		{ KRILIN_SKIN_FILENAME, "Krilin" },
		{ "", "Hello World!" }
	};

	Dictionary<string, float> guiPhotoYcoords = new Dictionary<string, float>()
	{
		{ GOKU_SKIN_FILENAME, 0 },
		{ BULMA_SKIN_FILENAME, 128 },
		{ VEGETA_SKIN_FILENAME, 64 },
		{ KRILIN_SKIN_FILENAME, 192 },
		{ "", 0 }
	};

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if (tempStr == "" || tempStr == null)
		// {
		// 	// avatar.regionEnable = true;
		// 	tempStr = PlayerSingleton.Instance.name;
		// 	avatar.SetRegionRect(new Rect2(
		// 		new Vector2(0, guiPhotoYcoords[tempStr]),
		// 		new Vector2(64, 64)
		// 	));

		// 	// debug
		// }
		//GD.Print(tempStr ?? "null");

		// nameLabel.Text = guiName[tempStr];

		dragonballCounterLabel.Text = GameSingleton.Instance.GetPointsString();
	}
}
