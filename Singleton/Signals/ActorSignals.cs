using System;
using Godot;

public partial class ActorSignals : Node
{
    public static ActorSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private ActorSignals() { }
    public static ActorSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new ActorSignals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void BehindActorEventHandler(string emitterName, string targetName);
}