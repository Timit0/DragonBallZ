using Godot;
using System;

public partial class SoundManagerAutoload : Node
{
	[Export]
	public AudioStreamPlayer MenuMusicPlayer {get;set;}

	[Export]
	public AudioStreamPlayer PressedButtonPlayer {get;set;}

	public override void _Ready()
	{
		this.MenuMusicPlayer.Finished += on_audio_stream_player_finished;
		this.MenuMusicPlayer.VolumeDb = SettingsDbContext.Instance.Get().SoundVolume;
		this.MenuMusicPlayer.Play();

		SettingsSingal.Instance.SoundVolumeChanged += on_sound_volume_changed;

		SoundManagerAutoloadSignals.Instance.PlayPressedButtonSound += on_play_pressed_button_sound;
	}

    private void on_audio_stream_player_finished()
    {
        this.MenuMusicPlayer.Play();
    }

	private void on_sound_volume_changed(float newValue)
    {
        this.MenuMusicPlayer.VolumeDb = newValue;
    }

	private void on_play_pressed_button_sound()
    {
        this.PressedButtonPlayer.Play();
    }
}
