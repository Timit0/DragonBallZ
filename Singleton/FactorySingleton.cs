using System;
using Godot;

public partial class FactorySingleton : Node
{
    public static FactorySingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private FactorySingleton() { }
    public static FactorySingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new FactorySingleton();
                }
            }
            return instance;
        }
    }

    public T GetThisNodeInstantiate<T>(Resource resource) where T : Node
    {
        PackedScene packedScene;
        packedScene = ResourceLoader.Load<PackedScene>(resource.ResourcePath);

        return packedScene.Instantiate<T>();
    }

    public T GetThisNodeInstantiateFromString<T>(string s) where T : Node
    {
        PackedScene packedScene;
        packedScene = ResourceLoader.Load<PackedScene>(s);

        return packedScene.Instantiate<T>();
    }

    public Player AddPlayerWithThisId(int id)
    {
        PackedScene packedScene;
        Player player = new Player();
        packedScene = ResourceLoader.Load<PackedScene>("res://Actors/Player/Player.tscn");
        player = packedScene.Instantiate<Player>();
        player.Name = id.ToString();
        
        return player;
    }
}