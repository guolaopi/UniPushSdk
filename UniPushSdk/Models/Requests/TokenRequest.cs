using Newtonsoft.Json;

namespace UniPushSdk.Models.Requests
{
    /// <summary>
    /// 请求token参数
    /// </summary>
    internal class TokenRequest
    {
        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("appkey")]
        public string AppKey { get; set; }
    }
}
