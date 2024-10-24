using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public class ApiResponse
{
    public bool success { get; set; }
    public string message { get; set; }
}