using System;
using Godot;

public partial class ActionSignals : Node
{
    public static ActionSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private ActionSignals() { }
    public static ActionSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new ActionSignals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void UpdateActionEventEventHandler();
}