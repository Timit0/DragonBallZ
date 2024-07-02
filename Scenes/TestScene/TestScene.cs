using Godot;
using System;

public partial class TestScene : MultiplayerScene
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

    public override void _Process(double delta)
    {
		foreach (Node node in GetChildren())
		{
			if(node is Player)
			{
				// GD.Print(node.GetMultiplayerAuthority());
			}
		}
        base._Process(delta);
    }
}
