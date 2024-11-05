using Godot;
using System;

public partial class ApiOptionsMenu : Control
{
	[Export]
	protected LineEdit lineEdit { get; set; }
	[Export]
	protected Button backButton { get; set; }
	[Export]
	protected string buttonTarget { get; set; }

	public override void _Ready()
	{
		backButton.Pressed += on_back_button_pressed;

		lineEdit.Text = SettingsDbContext.Instance.Get().UrlApi;
		base._Ready();
	}

	private async void on_back_button_pressed()
	{
		try
		{
			new Uri($"http://{lineEdit.Text}:8080");
		}
		catch (Exception e)
		{
			string message = "The URL is BAD, try again";
			Notification.NOTIFICATION_ENUM notificationType = global::Notification.NOTIFICATION_ENUM.ERROR;
			NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), message, notificationType.ToString());
			return;
		}
		// ApiSingleton.Instance.OverrideIp(lineEdit.Text);
		await SettingsDbContext.Instance.UpdateConnection(urlApi: lineEdit.Text);
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), buttonTarget);
	}

}
