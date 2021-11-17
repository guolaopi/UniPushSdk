using Newtonsoft.Json;

namespace UniPushSdk.Models.Requests
{
    /// <summary>
    /// 全体推送请求参数
    /// </summary>
    public class AllPushRequest : BasePushRequest
    {
        /// <summary>
        /// 推送目标用户，写死填 all
        /// </summary>
        [JsonProperty("audience")]
        public new string Audience = "all";
    }
}
