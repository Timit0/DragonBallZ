using System;
using Godot;

public partial class SceneSignals : Node
{
    public static SceneSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private SceneSignals() { }
    public static SceneSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new SceneSignals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void ChangeToThisSceneEventHandler(string scenePath);
}