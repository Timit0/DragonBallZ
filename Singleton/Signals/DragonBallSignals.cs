using System;
using Godot;

public partial class DragonBallSignals : Node
{
    public static DragonBallSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private DragonBallSignals() { }
    public static DragonBallSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new DragonBallSignals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void DragonBallIsRemovedEventHandler(string dBallName);
}