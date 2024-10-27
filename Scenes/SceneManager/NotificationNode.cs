using Godot;
using System;

public partial class NotificationNode : CanvasLayer
{
	[Export]
	protected Resource resource { get; set; }

	[Export]
	protected Node2D spawnPoint { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NotificationSignals.Instance.ShowNotification += on_show_notification;
	}

    private void on_show_notification(string content, string notificationType)
    {
		Enum.TryParse(notificationType, out Notification.NOTIFICATION_ENUM notifType);
        Notification notification = FactorySingleton.Instance.GetThisNodeInstantiate<Notification>(resource);
		notification.Content = content;
		notification.Type = notifType;
		spawnPoint.AddChild(notification);
    }

    // private void on_show_notification(string content)
	// {
	// 	Notification notification = FactorySingleton.Instance.GetThisNodeInstantiate<Notification>(resource);
	// 	notification.Content = content;
	// 	spawnPoint.AddChild(notification);
	// }

}
