using System;
using System.Collections.Generic;
using Godot;

public partial class GameSingleton : Node
{
    public static GameSingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private GameSingleton() { }
    public static GameSingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new GameSingleton();
                }
            }
            return instance;
        }
    }

    public int Points { get; set; } = 0;

    public string GetPointsString()
    {
        return $"{Points}/7";
    }
}