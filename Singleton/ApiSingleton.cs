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
    protected string ip { get; set; } = "http://157.26.121.64:8080";

    private System.Net.Http.HttpClient _httpClient { get; set; } = null;
    protected System.Net.Http.HttpClient httpClient
    {
        get
        {
            if (_httpClient is null)
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
        if (apiResponse.Success)
        {
            notificationType = global::Notification.NOTIFICATION_ENUM.SUCCES;
            NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.Message, notificationType.ToString());
        }
        else
        {
            notificationType = global::Notification.NOTIFICATION_ENUM.ERROR;
            NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.Message, notificationType.ToString());
        }

        return apiResponse.Success;
    }

    public async Task<bool> PostOnApiWithNotification(string apiRoute, FormUrlEncodedContent dataToSend, bool showNotification = true)
    {

        HttpResponseMessage response = await this.httpClient.PostAsync(apiRoute, dataToSend);
        string responseBody = await response.Content.ReadAsStringAsync();
        GD.Print(responseBody);
        ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        Notification.NOTIFICATION_ENUM notificationType;

        if (showNotification)
        {
            if (apiResponse.Success)
            {
                notificationType = global::Notification.NOTIFICATION_ENUM.SUCCES;
                NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.Message, notificationType.ToString());
            }
            else
            {
                notificationType = global::Notification.NOTIFICATION_ENUM.ERROR;
                NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), apiResponse.Message, notificationType.ToString());
            }
        }

        return apiResponse.Success;
    }

    public async Task<List<HostServer>> PostOnApiGetAllAvailableHostServer()
    {
        HttpResponseMessage response = await this.httpClient.PostAsync("/get_all_available_host_server", null);
        string responseBody = await response.Content.ReadAsStringAsync();
        GD.Print(responseBody);
        ApiResponseHostServer apiResponse = JsonConvert.DeserializeObject<ApiResponseHostServer>(responseBody);
        List<HostServer> hostServers = JsonConvert.DeserializeObject<List<HostServer>>(apiResponse.HostServerListString);

        return hostServers;
    }
}