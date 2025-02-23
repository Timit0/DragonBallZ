using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
public partial class LogInForm : ConnectionFormOverride
{
	[Export]
	protected Resource resource { get; set; }

	protected override async void on_action_button_pressed()
	{
		if (lineEditPassword.Text == "" || lineEditUsername.Text == "")
		{
			return;
		}

		dataToSend = new FormUrlEncodedContent(
			new[]
			{
				new KeyValuePair<string, string>("username", this.lineEditUsername.Text),
				new KeyValuePair<string, string>("password", this.lineEditPassword.Text),
			}
		);

		bool apiSuccess = await ApiSingleton.Instance.PostOnApiWithNotification("/logIn", dataToSend);

		if (apiSuccess)
		{
			UserSingleton.Instance.User.Username = lineEditUsername.Text;
			SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), resource.ResourcePath);
		}
	}
}
