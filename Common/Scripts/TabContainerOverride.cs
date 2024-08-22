using Godot;
using System;

public partial class TabContainerOverride : TabContainer
{
	public override void _Ready()
	{
		this.TabChanged += on_tab_changed;
	}

	private void on_tab_changed(long tab)
	{
		SoundManagerAutoloadSignals.Instance.EmitSignal(nameof(SoundManagerAutoloadSignals.Instance.ButtonPressedSoundPlay), (int)global::ButtonOverride.BUTTON_SOUND.NEXT);
	}

}
