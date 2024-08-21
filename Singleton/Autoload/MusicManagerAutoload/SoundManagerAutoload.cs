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
		this.MenuMusicPlayer.VolumeDb = SettingsDbContext.Instance.Get().MusicVolume;
		SetANewMusicMenu();
		this.MenuMusicPlayer.Play();

		this.PressedButtonPlayer.VolumeDb = SettingsDbContext.Instance.Get().UIVolume;

		SettingsSingal.Instance.MusicVolumeChanged += on_sound_volume_changed;

		SoundManagerAutoloadSignals.Instance.ButtonPressedSoundPlay += on_button_pressed_sound_play;
		SettingsSingal.Instance.UIVolumeChanged += on_button_pressed_sound_volume_changed;
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

	private void on_button_pressed_sound_play(int btn)
	{
		this.PressedButtonPlayer.Play();
	}

	private void on_button_pressed_sound_volume_changed(float newValue)
	{
		this.PressedButtonPlayer.VolumeDb = newValue;
	}

	public void SetANewMusicMenu()
	{
		int lenght = MenuMusics.Count;

		if (lenght <= 1)
		{
			MenuMusicPlayer.Stream = MenuMusics[0];
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

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustReleased("mouse_left_click"))
		{
			// SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.PlayPressedButtonSound));
		}
		base._Input(@event);
	}
}
