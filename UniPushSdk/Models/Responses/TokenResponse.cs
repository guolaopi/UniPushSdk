using Newtonsoft.Json;

namespace UniPushSdk.Models.Responses
{
    /// <summary>
    /// 获取token的结果
    /// </summary>
    internal class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// token过期时间，ms时间戳
        /// </summary>
        [JsonProperty("expire_time")]
        public string ExpireTime { get; set; }
    }
}
