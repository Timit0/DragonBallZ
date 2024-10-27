using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public partial class ApiSingleton
{
    public static ApiSingleton instance = null;
    protected static readonly object threadSafeLocker = new object();
    private ApiSingleton() { }
    public static ApiSingleton Instance
    {
        get
        {
            lock (threadSafeLocker)
            {
                if (instance == null)
                {
                    instance = new ApiSingleton();
                }
            }
            return instance;
        }
    }
    protected string ip {get;set;} = "http://127.0.0.1:8080";

    private System.Net.Http.HttpClient _httpClient {get;set;} = null;
    protected System.Net.Http.HttpClient httpClient 
    {
        get
        {
            if(_httpClient is null)
            {
                _httpClient = new System.Net.Http.HttpClient();
                _httpClient.BaseAddress = new Uri(this.ip);
            }
            return _httpClient;
        }
        set => _httpClient = value;
    }


    public async Task<bool> PostOnApiWithNotification(string apiRoute, FormUrlEncodedContent dataToSend)
    {
        
		HttpResponseMessage response = await this.httpClient.PostAsync(apiRoute, dataToSend);
		string responseBody = await response.Content.ReadAsStringAsync();
        GD.Print(responseBody);
		ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
		Notification.NOTIFICATION_ENUM notificationType;
		if(apiResponse.success)
		{
			notificationType=global::Notification.NOTIFICATION_ENUM.SUCCES;
			NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.message, notificationType.ToString());
		}else
		{
			notificationType=global::Notification.NOTIFICATION_ENUM.ERROR;
			NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.message, notificationType.ToString());
		}

        return apiResponse.success;
    }

    // public async void PostOnApiWithNotificationAndChangeToScene(string apiRoute, FormUrlEncodedContent dataToSend, string scenePath, bool successMode = true)
    // {
        
	// 	HttpResponseMessage response = await this.httpClient.PostAsync(apiRoute, dataToSend);
	// 	string responseBody = await response.Content.ReadAsStringAsync();
	// 	ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
	// 	Notification.NOTIFICATION_ENUM notificationType;
	// 	if(apiResponse.success)
	// 	{
	// 		notificationType=global::Notification.NOTIFICATION_ENUM.SUCCES;
	// 		NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.message, notificationType.ToString());
	// 	}else
	// 	{
	// 		notificationType=global::Notification.NOTIFICATION_ENUM.ERROR;
	// 		NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.message, notificationType.ToString());
	// 	}

    //     if(apiResponse.success == successMode)
    //     {
    //         SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), scenePath);
    //     }
    // }
}