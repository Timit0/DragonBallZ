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
	protected Button returnAndSaveButton { get; set; }
	[Export]
	protected Button resetButton { get; set; }

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

		returnAndSaveButton.Pressed += on_returnAndSave_button_pressed;
		resetButton.Pressed += on_reset_button_pressed;
	}

	private void on_music_volume_slider_change(double value)
	{
		SettingsSingal.Instance.EmitSignal(nameof(SettingsSingal.Instance.MusicVolumeChanged), (float)value);
	}

	private void on_ui_volume_slider_change(double value)
	{
		SettingsSingal.Instance.EmitSignal(nameof(SettingsSingal.Instance.UIVolumeChanged), (float)value);
	}

	private async void on_returnAndSave_button_pressed()
	{
		SettingsSingal.Instance.EmitSignal(nameof(SettingsSingal.Instance.SaveChange));
		await SettingsDbContext.Instance.Update(musicV: (float)musicVolumeSlider.Value, uIV: (float)uIVolumeSLider.Value);
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), returnTarget);
	}

	private void on_reset_button_pressed()
	{
		// musicVolumeSlider.Value = -25;
		// uIVolumeSLider.Value = 0;

		ResetActionEvent("move_up", Key.W);
		ResetActionEvent("move_down", Key.S);
		ResetActionEvent("move_left", Key.A);
		ResetActionEvent("move_right", Key.D);

		ResetActionEvent("collect_dragon_ball", Key.F);
		ResetActionEvent("zoomed_camera", Key.Shift);

		ResetActionEvent("pause_menu", Key.Escape);
		ResetActionEvent("dragon_radar", Key.R);

		ActionSignals.Instance.EmitSignal(nameof(ActionSignals.Instance.UpdateActionEvent));
	}

	private void ResetActionEvent(string actionName, Godot.Key key)
	{
		InputEventKey inputEventKey = new InputEventKey();
		inputEventKey.Keycode = key;
		InputMap.ActionEraseEvents(actionName);
		InputMap.Singleton.ActionAddEvent(actionName, inputEventKey);
	}
}
