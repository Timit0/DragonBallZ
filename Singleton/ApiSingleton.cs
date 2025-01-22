using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
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

    private System.Net.Http.HttpClient _httpClient { get; set; } = null;
    protected System.Net.Http.HttpClient httpClient
    {
        get
        {
            if (_httpClient is null)
            {
                _httpClient = new System.Net.Http.HttpClient()
                {
                    BaseAddress = new Uri(this.Ip),
                    Timeout = TimeSpan.FromSeconds(1.25)
                };
            }
            return _httpClient;
        }
        set => _httpClient = value;
    }

    public string Ip { get; set; } = "";

    public void OverrideIp(string ip, string port = "8080")
    {
        Ip = $"http://{ip}:{port}";

        //Dispose because it's possible to attempt an old request
        _httpClient?.Dispose();
        _httpClient = new System.Net.Http.HttpClient
        {
            BaseAddress = new Uri(Ip),
            Timeout = TimeSpan.FromSeconds(1.25)
        };
    }

    public async Task<bool> PostOnApiWithNotification(string apiRoute, FormUrlEncodedContent dataToSend)
    {
        try
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
        catch (Exception e)
        {
            string message = "The URL is BAD, try to change it in \"Options API\"";
            Notification.NOTIFICATION_ENUM notificationType = global::Notification.NOTIFICATION_ENUM.ALERT;
            NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), message, notificationType.ToString());
            return false;
        }
    }

    public async Task<bool> PostOnApiWithNotification(string apiRoute, FormUrlEncodedContent dataToSend, bool showNotification = true)
    {
        try
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
        catch (Exception e)
        {
            string message = "The URL is BAD, try to change it in \"Options API\"";
            Notification.NOTIFICATION_ENUM notificationType = global::Notification.NOTIFICATION_ENUM.ALERT;
            NotificationSignals.Instance.EmitSignal(nameof(NotificationSignals.Instance.ShowNotification), message, notificationType.ToString());
            return false;
        }
    }

    public async Task<List<HostServer>> PostOnApiGetAllHostServer()
    {
        HttpResponseMessage response = await this.httpClient.PostAsync("/get_all_host_server", null);
        string responseBody = await response.Content.ReadAsStringAsync();
        GD.Print(responseBody);
        ApiResponseHostServer apiResponse = JsonConvert.DeserializeObject<ApiResponseHostServer>(responseBody);
        List<HostServer> hostServers = JsonConvert.DeserializeObject<List<HostServer>>(apiResponse.HostServerListString);

        return hostServers;
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