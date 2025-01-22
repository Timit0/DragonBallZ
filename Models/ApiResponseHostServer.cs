using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

public class ApiResponseHostServer : ApiResponse
{

    [JsonProperty(PropertyName = "host_server_list")]
    public string HostServerListString {get;set;}
}