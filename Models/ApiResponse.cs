using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

public class ApiResponse
{
    [JsonProperty(PropertyName = "success")]
    public bool Success { get; set; }
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }
}