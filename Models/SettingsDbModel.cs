public class SettingsDbModel
{
    public int Id { get; set; }
    public float MusicVolume { get; set; }
    public float UIVolume { get; set; }

    public string MoveUp { get; set; }
    public string MoveDown { get; set; }
    public string MoveLeft { get; set; }
    public string MoveRight { get; set; }

    public string CollectDragonBall { get; set; }
    public string ZoomedCamera { get; set; }

    public string PauseMenu { get; set; }
    public string DragonRadar { get; set; }
}