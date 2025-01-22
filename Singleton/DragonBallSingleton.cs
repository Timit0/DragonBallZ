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

    // public List<Vector2> DragonBallListOfPositions { get; set; } = new List<Vector2>();

    public Dictionary<string, DragonBallOnRadarModel> DragonBallPosition { get; set; } = new Dictionary<string, DragonBallOnRadarModel>()
    {
        {
            "DragonBall0", new DragonBallOnRadarModel()
        },
        {
            "DragonBall1", new DragonBallOnRadarModel()
        },
        {
            "DragonBall2", new DragonBallOnRadarModel()
        },
        {
            "DragonBall3", new DragonBallOnRadarModel()
        },
        {
            "DragonBall4", new DragonBallOnRadarModel()
        },
        {
            "DragonBall5", new DragonBallOnRadarModel()
        },
        {
            "DragonBall6", new DragonBallOnRadarModel()
        }
    };

    public void ResetDragonBAll()
    {
        for (int i = 0; i < 7; i++)
        {
            DragonBallPosition[$"DragonBall{i}"].Exist = true;
        }
    }
}