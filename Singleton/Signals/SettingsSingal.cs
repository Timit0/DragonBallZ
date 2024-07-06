using System;
using Godot;

public partial class SettingsSingal : Node
{
    public static SettingsSingal instance = null;
    protected static readonly object threadSafeLocker = new object();
    private SettingsSingal() { }
    public static SettingsSingal Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new SettingsSingal();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void SoundVolumeChangedEventHandler(float newValue);
}