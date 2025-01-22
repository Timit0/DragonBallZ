using System;
using Godot;

public partial class CameraSignals : Node
{
    public static CameraSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private CameraSignals() { }
    public static CameraSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new CameraSignals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void ActiveThisCameraEventHandler(int enumInt);
}