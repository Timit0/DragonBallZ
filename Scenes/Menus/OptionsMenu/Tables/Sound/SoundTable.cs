using Godot;
using System;

public partial class SoundTable : Control
{
    [Export]
    public Slider SliderMusicVolume { get; set; }

    [Export]
    public Slider SliderUIVolume { get; set; }

    [Export]
    public Slider SliderTransitionVolume { get; set; }

    public override void _Ready()
    {
        SliderUIVolume.DragEnded += on_slider_ui_volume_draged_end;

        SliderTransitionVolume.DragEnded += on_slider_transition_drag_end;

        base._Ready();
    }

    private void on_slider_ui_volume_draged_end(bool valueChanged)
    {
        SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.ButtonPressedSoundPlay), (int)global::ButtonOverride.BUTTON_SOUND.NEXT);
    }

    private void on_slider_transition_drag_end(bool valueChanged)
    {
        SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.TransitionSoundPlay));
    }
}
