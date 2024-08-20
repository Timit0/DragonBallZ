using Godot;
using System;

public partial class SoundManagerAutoload : Node
{
	[ExportGroup("Musics")]
	[Export]
	public Godot.Collections.Array<AudioStreamWav> MenuMusics { get; set; }

	[ExportGroup("Nodes")]
	[Export]
	public AudioStreamPlayer MenuMusicPlayer { get; set; }

	[Export]
	public AudioStreamPlayer PressedButtonPlayer { get; set; }

	protected int lastMenuMusicsIndex { get; set; } = -1;

	public override void _Ready()
	{
		this.MenuMusicPlayer.Finished += on_audio_stream_player_finished;
		this.MenuMusicPlayer.VolumeDb = SettingsDbContext.Instance.Get().SoundVolume;
		SetANewMusicMenu();
		this.MenuMusicPlayer.Play();

		SettingsSingal.Instance.SoundVolumeChanged += on_sound_volume_changed;

		SoundManagerAutoloadSignals.Instance.PlayPressedButtonSound += on_play_pressed_button_sound;
	}

	private void on_audio_stream_player_finished()
	{
		SetANewMusicMenu();
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

	public void SetANewMusicMenu()
	{
		int lenght = MenuMusics.Count;
		GD.Print(lenght);

		if (lenght <= 1)
		{
			MenuMusicPlayer.Stream = MenuMusics[0];
			GD.Print("FDP");
			return;
		}

		Random random = new Random();
		while (true)
		{
			int rndMusic;
			rndMusic = random.Next(0, lenght);
			if (rndMusic != lastMenuMusicsIndex)
			{
				MenuMusicPlayer.Stream = MenuMusics[rndMusic];
				lastMenuMusicsIndex = rndMusic;
				return;
			}
		}
	}
}
