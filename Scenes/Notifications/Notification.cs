using Godot;
using System;

public partial class Notification : Node2D
{
	[Export]
	protected AnimationPlayer animationPlayer { get; set; }
	[Export]
	protected Label label { get; set; }
	[Export]
	protected Panel panel { get; set; }

	public enum NOTIFICATION_ENUM
	{
		ERROR,
		SUCCES,
		ALERT
	}

	public NOTIFICATION_ENUM Type { get; set; }

	public string Content { get; set; }

	public override void _Ready()
	{
		this.label.Text = Content;
		animationPlayer.Play("RESET");
		SetByType(this.Type);
	}

	protected void SetByType(NOTIFICATION_ENUM type)
	{
		switch (type)
		{
			case NOTIFICATION_ENUM.ERROR:
				panel.Modulate = new Color(1, 0, 0);
				return;
			case NOTIFICATION_ENUM.SUCCES:
				panel.Modulate = new Color(0, 1, 0);
				return;
			case NOTIFICATION_ENUM.ALERT:
				panel.Modulate = new Color(1, 1, 0);
				return;
		}
	}
}
