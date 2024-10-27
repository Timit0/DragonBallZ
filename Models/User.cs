using Newtonsoft.Json;

public class User
{
    [JsonProperty(PropertyName = "id")]
    public int Id {get;set;}
    [JsonProperty(PropertyName = "username")]
    public string Username {get;set;}
    [JsonProperty(PropertyName = "password")]
    public string Password {get;set;}
}