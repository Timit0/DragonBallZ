using Godot;
using System;

public partial class SceneManager : Node
{
	[Export]
	public Resource SceneToLoad { get; set; }

	[Export]
	public Node SceneNode { get; set; }


	public override void _Ready()
	{
		SceneNode.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Node>(SceneToLoad));

		SceneSignals.Instance.ChangeToThisScene += on_change_to_this_scene;

		LoadInput("MoveUp", "move_up");
		LoadInput("MoveDown", "move_down");
		LoadInput("MoveLeft", "move_left");
		LoadInput("MoveRight", "move_right");
		LoadInput("CollectDragonBall", "collect_dragon_ball");
		LoadInput("ZoomedCamera", "zoomed_camera");
		LoadInput("PauseMenu", "pause_menu");
		LoadInput("DragonRadar", "dragon_radar");
	}

	private void on_change_to_this_scene(string scenePath)
	{
		GetTree().Paused = false;
		RemoveScene();
		SceneNode.AddChild(FactorySingleton.Instance.GetThisNodeInstantiateFromString<Node>(scenePath));
	}

	protected void RemoveScene()
	{
		foreach (Node node in SceneNode.GetChildren())
		{
			SceneNode.RemoveChild(node);
			// node.QueueFree();
		}
	}

	protected void LoadInput(string actionNameInDB, string actionName)
	{
		InputMap.Singleton.ActionEraseEvents(actionName);
		string s = SettingsDbContext.Instance.Get(actionNameInDB).ToString();
		InputEventKey inputEventKey = new InputEventKey();
		Enum.TryParse(s, out Key key);
		inputEventKey.Keycode = key;

		InputMap.Singleton.ActionAddEvent(actionName, inputEventKey);
	}
}
