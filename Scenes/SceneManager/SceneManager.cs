using Godot;
using System;
using System.Collections.Generic;
using System.Net.Http;

public partial class SceneManager : Node
{
	[Export]
	public Resource SceneToLoad { get; set; }

	[Export]
	public Node SceneNode { get; set; }

	[Export]
	public AnimationPlayer TransitionAnimationPlayer { get; set; }

	protected string scenePathToLoad { get; set; }

	public override void _Ready()
	{
		SceneNode.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Node>(SceneToLoad));

		SceneSignals.Instance.ChangeToThisScene += on_change_to_this_scene;

		LoadInputs();
		SoundManagerAutoload.Instance.MusicPlay(true);
	}

	public async override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest)
		{
			FormUrlEncodedContent dataToSend = new FormUrlEncodedContent(
				new[]
				{
					new KeyValuePair<string, string>("username", UserSingleton.Instance.User.Username),
				}
			);

			GD.Print("QUITING");
			await ApiSingleton.Instance.PostOnApiWithNotification("/delete_host_server", dataToSend, false);
		}
	}

    private void on_change_to_this_scene(string scenePath)
	{
		GetTree().Paused = false;
		scenePathToLoad = scenePath;

		TransitionAnimationPlayer.Stop();
		TransitionAnimationPlayer.Play("Transition");
	}

	public void ChangeScene()
	{
		RemoveScene();
		SceneNode.AddChild(FactorySingleton.Instance.GetThisNodeInstantiateFromString<Node>(scenePathToLoad));
	}

	protected void RemoveScene()
	{
		foreach (Node node in SceneNode.GetChildren())
		{
			SceneNode.RemoveChild(node);
			// node.QueueFree();
		}
	}

	public void LoadInputs()
	{
		LoadInput("MoveUp", "move_up");
		LoadInput("MoveDown", "move_down");
		LoadInput("MoveLeft", "move_left");
		LoadInput("MoveRight", "move_right");
		LoadInput("CollectDragonBall", "collect_dragon_ball");
		LoadInput("ZoomedCamera", "zoomed_camera");
		LoadInput("PauseMenu", "pause_menu");
		LoadInput("DragonRadar", "dragon_radar");
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

	public void EmitTransitionSound()
	{
		SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.TransitionSoundPlay));
	}
}
