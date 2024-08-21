using Godot;
using System;

public partial class OptionsMenu : Control
{
	[Export]
	protected string returnTarget { get; set; }

	[ExportGroup("Nodes")]
	[Export]
	protected SoundTable soundTable { get; set; }
	[Export]
	protected Button returnButton { get; set; }

	protected Slider musicVolumeSlider
	{
		get => soundTable.SliderMusicVolume;
		set => soundTable.SliderMusicVolume = value;
	}

	protected Slider uIVolumeSLider
	{
		get => soundTable.SliderUIVolume;
		set => soundTable.SliderUIVolume = value;
	}

	public override void _Ready()
	{
		musicVolumeSlider.Value = SettingsDbContext.Instance.Get().MusicVolume;
		musicVolumeSlider.ValueChanged += on_music_volume_slider_change;

		uIVolumeSLider.Value = SettingsDbContext.Instance.Get().UIVolume;
		uIVolumeSLider.ValueChanged += on_ui_volume_slider_change;

		returnButton.Pressed += on_return_button_pressed;
	}

	private void on_music_volume_slider_change(double value)
	{
		SettingsSingal.Instance.EmitSignal(nameof(SettingsSingal.Instance.MusicVolumeChanged), (float)value);
	}

	private void on_ui_volume_slider_change(double value)
	{
		SettingsSingal.Instance.EmitSignal(nameof(SettingsSingal.Instance.UIVolumeChanged), (float)value);
	}

	private async void on_return_button_pressed()
	{
		await SettingsDbContext.Instance.Update(musicV: (float)musicVolumeSlider.Value, uIV: (float)uIVolumeSLider.Value);
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), returnTarget);
	}
}
