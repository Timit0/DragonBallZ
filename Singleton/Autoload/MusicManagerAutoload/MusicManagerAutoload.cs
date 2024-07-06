using Godot;
using System;

public partial class MusicManagerAutoload : Node
{
	[Export]
	public AudioStreamPlayer AudioStreamPlayer {get;set;}

	public override void _Ready()
	{
		this.AudioStreamPlayer.Finished += on_audio_stream_player_finished;
		this.AudioStreamPlayer.VolumeDb = SettingsDbContext.Instance.Get().SoundVolume;
		this.AudioStreamPlayer.Play();

		SettingsSingal.Instance.SoundVolumeChanged += on_sound_volume_changed;
	}

    private void on_audio_stream_player_finished()
    {
        this.AudioStreamPlayer.Play();
    }

	private void on_sound_volume_changed(float newValue)
    {
        this.AudioStreamPlayer.VolumeDb = newValue;
    }
}
