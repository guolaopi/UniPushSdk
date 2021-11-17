using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniPushSdk.Models
{
    /// <summary>
    /// 推送目标用户
    /// </summary>
    public class AudienceModel
    {
        /// <summary>
        /// cid数组，推送单一消息时只能填一个cid
        /// </summary>
        [JsonProperty("cid")]
        public List<string> CId { get; set; }
    }
}
