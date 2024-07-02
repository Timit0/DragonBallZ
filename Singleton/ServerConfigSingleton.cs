using System;
using Godot;

public class ServerConfigSingleton
{
    public static ServerConfigSingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private ServerConfigSingleton() { }
    public static ServerConfigSingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new ServerConfigSingleton();
                }
            }
            return instance;
        }
    }

    public enum ConfigServerEnum
    {
        HOST,
        JOIN,
    }

    public ConfigServerEnum ServerMode {get;set;}
    public string IpAdresse {get;set;}
}