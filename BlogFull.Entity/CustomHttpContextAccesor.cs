using Microsoft.AspNetCore.Http;

namespace BlogFull.Entity
{
    public interface ICustomHttpContextAccessor : IHttpContextAccessor
    {
        string GetHeaders();

        string GetHeaderValue(string key);

        int GetUserId();

        string GetRemoteIpAddress();
    }

    public class CustomHttpContextAccessor : HttpContextAccessor, ICustomHttpContextAccessor
    {
        public string GetHeaderValue(string key)
        {
            HttpContext.Request.Headers.TryGetValue(key, out var names);
            return names.FirstOrDefault();
        }

        public int GetUserId() => int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value, out int id) ? id : default;

        public string GetHeaders()
        {
            string headers = null;
            foreach (var key in HttpContext.Request.Headers.Keys)
                headers += $"{key} = {HttpContext.Request.Headers[key]}{Environment.NewLine}";
            return headers;
        }

        public string GetRemoteIpAddress() => HttpContext.Connection.RemoteIpAddress.ToString();
    }
}
