using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class SpawnerPoint : Node2D
{
    [Export]
    protected Resource resource;

    [Export]
    protected int amount { get; set; }

    [Export(PropertyHint.Range, "0,1000")]
    protected int randomRange { get; set; } = 200;

    [Export]
    protected string name { get; set; }

    [Export]
    protected bool isSync { get; set; } = false;

    public override void _Ready()
    {
        if (isSync && !this.IsMultiplayerAuthority())
        {
            return;
        }

        Random random = new Random();

        for (int i = 0; i < amount; i++)
        {
            int x = random.Next(-randomRange, randomRange + 1);
            int y = random.Next(-randomRange, randomRange + 1);
            GD.Print("X " + x + " Y " + y);

            Node2D node2D = FactorySingleton.Instance.GetThisNodeInstantiate<Node2D>(resource);
            node2D.Position = new Vector2(this.Position.X + x, this.Position.Y + y);
            node2D.Name = name + i;

            this.Owner.CallDeferred("add_child", node2D, isSync);
        }
        base._Ready();
    }
}
