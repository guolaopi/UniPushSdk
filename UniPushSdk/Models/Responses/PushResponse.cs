using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniPushSdk.Models.Responses
{
    /// <summary>
    /// 推送结果
    /// </summary>
    public class PushResponse
    {
        [JsonProperty("$taskid")]
        public Dictionary<string, string> TaskId { get; set; }
    }
}
