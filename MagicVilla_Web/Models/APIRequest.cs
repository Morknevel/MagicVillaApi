using MagicVilla_Utility;

namespace MagicVilla_Web.Models;

public class APIRequest
{
    public SD.ApiType  ApiType { get; set; }
    public string Url { get; set; }
    public Object Data { get; set; }
}