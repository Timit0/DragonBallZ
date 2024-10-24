using System;
using Godot;

public partial class NotificationSignals : Node
{
    public static NotificationSignals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private NotificationSignals() { }
    public static NotificationSignals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new NotificationSignals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void ShowNotificationEventHandler(string content);
}