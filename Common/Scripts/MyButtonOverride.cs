using System;
using Godot;

public partial class MyButtonOverride : Button
{
    public override void _Ready()
    {
        this.Pressed += ont_this_button_pressed;
        base._Ready();
    }

    private void ont_this_button_pressed()
    {
        SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.PlayPressedButtonSound));
    }
}