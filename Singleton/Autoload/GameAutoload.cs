using Godot;
using System;

public partial class GameAutoload : Node
{

    public static GameAutoload Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    // [Rpc(MultiplayerApi.RpcMode.Authority, CallLocal = true)]
    // public void GetPointFromAuthRPC()
    // {
    //     GD.Print("PUTIANINSOIDOABSDOBUO");
    //     Rpc(nameof(this.ReceivePointsFromAuth), GameSingleton.Instance.GetPoints());
    // }

    // [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
    // public void ReceivePointsFromAuth(int x)
    // {
    //     GD.Print(x);
    //     GameSingleton.Instance.UpdatePoints(x);
    // }

    // public void UpdatePoints()
    // {
    //     Rpc(nameof(this.GetPointFromAuthRPC));
    // }
}
