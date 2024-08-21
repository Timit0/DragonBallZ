using Godot;

public partial class ButtonOverride : Button
{
    public enum BUTTON_SOUND
    {
        NEXT,
        PREV,
    }

    [Export]
    public BUTTON_SOUND ButtonSound { get; set; }

    public override void _Pressed()
    {
        SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.ButtonPressedSoundPlay), (int)ButtonSound);
        base._Pressed();
    }
}