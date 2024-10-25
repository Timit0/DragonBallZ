using System;
using Godot;

public partial class SoundManagerAutoloadSignals : Node
{
    public static SoundManagerAutoloadSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private SoundManagerAutoloadSignals() { }
    public static SoundManagerAutoloadSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new SoundManagerAutoloadSignals();
                }
            }
            return instance;
        }
    }

    // [Signal]
    // public delegate void TransitionSoundPlayEventHandler();

    [Signal]
    public delegate void ButtonPressedSoundPlayEventHandler(int btn);

    [Signal]
    public delegate void DragonBallSoundPlayEventHandler();

    [Signal]
    public delegate void TransitionSoundPlayEventHandler();
}