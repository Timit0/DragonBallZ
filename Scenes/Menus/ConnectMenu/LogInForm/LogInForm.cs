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
		dataToSend = new FormUrlEncodedContent(
			new[]
			{
				new KeyValuePair<string, string>("username", this.lineEditUsername.Text),
				new KeyValuePair<string, string>("password", this.lineEditPassword.Text),
			}
		);
		HttpResponseMessage response = await this.httpClient.PostAsync(this.apiRoute, dataToSend);
		string responseBody = await response.Content.ReadAsStringAsync();
		ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

		NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.message);

		if (apiResponse.success)
		{
			UserSingleton.Instance.Username = lineEditUsername.Text;
			SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), resource.ResourcePath);
		}
	}
}
