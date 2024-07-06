using Godot;
using System;

public partial class OptionsMenu : Control
{
	[Export]
	protected string returnTarget {get;set;}

	[ExportGroup("Nodes")]
	[Export]
	protected SoundTable soundTable {get;set;}
	[Export]
	protected Button returnButton {get;set;}

	protected Slider soundVolumeSlider
	{
		get => soundTable.SliderVolume;
		set => soundTable.SliderVolume = value;
	}

	public override void _Ready()
	{
		soundVolumeSlider.Value = SettingsDbContext.Instance.Get().SoundVolume;
		soundVolumeSlider.ValueChanged += on_sound_volume_slider_change;

		returnButton.Pressed += on_retutn_button_pressed;
	}

    private void on_sound_volume_slider_change(double value)
    {
		SettingsSingal.Instance.EmitSignal(nameof(SettingsSingal.Instance.SoundVolumeChanged), (float)value);
    }

	private async void on_retutn_button_pressed()
    {
        await SettingsDbContext.Instance.Update(soundV: (float)soundVolumeSlider.Value);
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), returnTarget);
    }
}
