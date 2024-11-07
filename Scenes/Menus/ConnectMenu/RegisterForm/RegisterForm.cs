using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

public partial class RegisterForm : ConnectionFormOverride
{
	protected async override void on_action_button_pressed()
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

		await ApiSingleton.Instance.PostOnApiWithNotification(this.apiRoute, dataToSend);
		base.on_action_button_pressed();
	}
}
