using System;
using Godot;

public partial class ServerSingals : Node
{
    public static ServerSingals instance = null;
    protected static readonly object threadSafeLocker = new object();
    private ServerSingals() { }
    public static ServerSingals Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new ServerSingals();
                }
            }
            return instance;
        }
    }

    [Signal]
    public delegate void CreateServerEventHandler();

    [Signal]
    public delegate void CloseServerEventHandler();

    [Signal]
    public delegate void CreateClientEventHandler();
}