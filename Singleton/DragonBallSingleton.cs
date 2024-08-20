using System;
using System.Collections.Generic;
using Godot;

public partial class DragonBallSingleton : Node
{
    public static DragonBallSingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private DragonBallSingleton() { }
    public static DragonBallSingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new DragonBallSingleton();
                }
            }
            return instance;
        }
    }

    public List<Vector2> DragonBallListOfPositions { get; set; } = new List<Vector2>();
}