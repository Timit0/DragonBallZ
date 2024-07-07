using Godot;
using System;

public partial class SceneManager : Node
{
	[Export]
	public Resource SceneToLoad {get;set;}

	[Export]
	public Node SceneNode {get;set;}


	public override void _Ready()
	{
		SceneNode.AddChild(FactorySingleton.Instance.GetThisNodeInstantiate<Node>(SceneToLoad));

		SceneSignals.Instance.ChangeToThisScene += on_change_to_this_scene;
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
		}
	}
}
