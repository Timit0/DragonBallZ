using Godot;
using System;

public partial class DragonBall : Node2D
{
    [Export]
    public Sprite2D Sprite { get; set; }
    [Export]
    public AnimationPlayer AnimationPlayer { get; set; }

    public override void _Ready()
    {
        AnimationPlayer.Play("RESET");
        this.ZIndex = (int)global::ZIndex.ZIndexEnum.DRAGON_BALL;
        DragonBallSingleton.Instance.DragonBallPosition[this.Name].Position = this.Position;
        DragonBallSingleton.Instance.DragonBallPosition[this.Name].Exist = true;
        base._Ready();
    }
}
