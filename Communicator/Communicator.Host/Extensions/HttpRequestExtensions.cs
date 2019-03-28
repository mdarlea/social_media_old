using System.Net.Http;
using System.Web;

namespace TwimlApi.Host.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string GetClientIp(this HttpRequestMessage request)
        {
            var ip = string.Empty;
            if (!request.Properties.ContainsKey("MS_HttpContext")) return ip;
            var context = (HttpContextBase)request.Properties["MS_HttpContext"];
            ip = context.Request.ServerVariables["HTTP_VIA"] != null
                    ? context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                    : context.Request.ServerVariables["REMOTE_ADDR"];
            return ip;
        }
    }
}