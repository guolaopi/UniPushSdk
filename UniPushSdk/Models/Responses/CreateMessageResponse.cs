using Newtonsoft.Json;

namespace UniPushSdk.Models.Responses
{
    /// <summary>
    /// 创建消息返回结果
    /// </summary>
    internal class CreateMessageResponse
    {
        [JsonProperty("taskid")]
        public string TaskId { get; set; }
    }
}
