using System;
using Godot;

public class UserSingleton
{
    public static UserSingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private UserSingleton() { }
    public static UserSingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new UserSingleton();
                }
            }
            return instance;
        }
    }

    public string Username {get;set;}
}