using Godot;
using System;

public partial class SoundManagerAutoload : Node
{
	public static SoundManagerAutoload Instance { get; private set; }

	[ExportGroup("Musics")]
	[Export]
	public Godot.Collections.Array<AudioStreamWav> MenuMusics { get; set; }

	[ExportGroup("Nodes")]
	[Export]
	public AudioStreamPlayer MenuMusicPlayer { get; set; }

	[Export]
	public AudioStreamPlayer PressedButtonPlayer { get; set; }

	[Export]
	public AudioStreamPlayer DragonBallCollectAudioStreamPlayer { get; set; }

	[Export]
	public AudioStreamPlayer TransitionAudioStreamPlayer { get; set; }

	protected int lastMenuMusicsIndex { get; set; } = -1;

	public override void _Ready()
	{
		Instance = this;

		this.MenuMusicPlayer.Finished += on_audio_stream_player_finished;

		SettingsSingals.Instance.MusicVolumeChanged += on_sound_volume_changed;

		SoundManagerAutoloadSignals.Instance.ButtonPressedSoundPlay += on_button_pressed_sound_play;
		SettingsSingals.Instance.UIVolumeChanged += on_uI_sound_volume_changed;

		SoundManagerAutoloadSignals.Instance.DragonBallSoundPlay += on_button_dragon_ball_sound_play;

		SoundManagerAutoloadSignals.Instance.TransitionSoundPlay += on_transition_sound_play;
		SettingsSingals.Instance.TransitionVolumeChanged += on_transition_volume_changed;

		this.SetUp();
	}

	private void on_audio_stream_player_finished()
	{
		SetANewMusicMenu();
		this.MenuMusicPlayer.Play();
	}

	private void on_sound_volume_changed(float newValue)
	{
		this.MenuMusicPlayer.VolumeDb = newValue;
		this.DragonBallCollectAudioStreamPlayer.VolumeDb = newValue;
	}

	private void on_button_pressed_sound_play(int btn)
	{
		this.PressedButtonPlayer.Play();
	}

	private void on_uI_sound_volume_changed(float newValue)
	{
		this.PressedButtonPlayer.VolumeDb = newValue;
	}

	private void on_button_dragon_ball_sound_play()
	{
		this.DragonBallCollectAudioStreamPlayer.Play();
	}

	private void on_transition_sound_play()
	{
		this.TransitionAudioStreamPlayer.Play();
	}

	private void on_transition_volume_changed(float newValue)
	{
		this.TransitionAudioStreamPlayer.VolumeDb = newValue;
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

	public void MusicPlay(bool state)
	{
		if (state)
		{
			MenuMusicPlayer.Play();
			return;
		}
		MenuMusicPlayer.Stop();
	}

	protected void SetUp()
	{
		this.MenuMusicPlayer.VolumeDb = SettingsDbContext.Instance.Get().MusicVolume;
		SetANewMusicMenu();

		this.PressedButtonPlayer.VolumeDb = SettingsDbContext.Instance.Get().UIVolume;

		this.TransitionAudioStreamPlayer.VolumeDb = SettingsDbContext.Instance.Get().TransitionVolume;
	}
}
