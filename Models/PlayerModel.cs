using System;
using Godot;

public class PlayerModel : ICloneable
{
    public Texture2D SkinTexture {get;set;}

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}