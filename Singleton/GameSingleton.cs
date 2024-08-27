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

    private int _points { get; set; } = 0;

    public void AddPoint()
    {
        _points++;
        if (_points == 7)
        {
            string scenePath = "res://Scenes/WinningScene/WinningScene.tscn";
            SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePath);
        }
    }

    public int GetPoints()
    {
        return _points;
    }

    public string GetPointsString()
    {
        return $"{_points}/7";
    }

    public void UpdatePoints(int points)
    {
        GD.Print("Update with " + points);
        _points = points;
    }
}