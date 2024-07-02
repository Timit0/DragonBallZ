using System;
using Godot;

public partial class PlayerSingleton : Node
{
    public static PlayerSingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private PlayerSingleton() { }
    public static PlayerSingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new PlayerSingleton();
                }
            }
            return instance;
        }
    }

    public PlayerModel PlayerModel {get;set;} = new PlayerModel();
}