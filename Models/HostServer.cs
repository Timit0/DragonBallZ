using Newtonsoft.Json;

public class HostServer
{
    [JsonProperty(PropertyName = "id")]
    public int Id {get;set;}
    [JsonProperty(PropertyName = "user_host")]
    public User UserHost {get;set;} = new User();
    [JsonProperty(PropertyName = "user_guest")]
    public User UserGuest {get;set;} = new User();
    [JsonProperty(PropertyName = "host_ip")]
    public string HostIp {get;set;}
}