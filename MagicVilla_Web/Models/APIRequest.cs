using MagicVilla_Utility;

namespace MagicVilla_Web.Models;

public class APIRequest
{
    public SD.ApiType  ApiType { get; set; }
    public string url { get; set; }
    public Object Data { get; set; }
}