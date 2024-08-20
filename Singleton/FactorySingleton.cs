using System;
using System.Collections.Generic;
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

    public PlayerSkinCase GetPlayerSkinCase(Texture2D texture)
    {
        PackedScene packedScene;
        packedScene = ResourceLoader.Load<PackedScene>("res://Scenes/SelectSkinMenu/PlayerSkinCase/PlayerSkinCase.tscn");
        PlayerSkinCase playerSkinCase = packedScene.Instantiate<PlayerSkinCase>();
        playerSkinCase.SkinTexture.Atlas = (playerSkinCase.PlayerModel.Clone() as PlayerModel).SkinTexture;
        playerSkinCase.PlayerModel.SkinTexture = texture;

        return playerSkinCase;
    }

    public DragonRadar GetDragonRadar()
    {
        PackedScene packedScene;
        packedScene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/DragonRadar/DragonRadar.tscn");
        DragonRadar dragonRadar = packedScene.Instantiate<DragonRadar>();

        return dragonRadar;
    }
}