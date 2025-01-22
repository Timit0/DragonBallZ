using System;
using Godot;

public partial class SettingsSingals : Node
{
    public static SettingsSingals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private SettingsSingals() { }
    public static SettingsSingals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new SettingsSingals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void MusicVolumeChangedEventHandler(float newValue);

    [Signal]
    public delegate void UIVolumeChangedEventHandler(float newValue);

    [Signal]
    public delegate void TransitionVolumeChangedEventHandler(float newValue);

    [Signal]
    public delegate void SaveChangeEventHandler();
}