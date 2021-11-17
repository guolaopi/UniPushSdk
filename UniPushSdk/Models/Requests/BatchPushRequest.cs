using Newtonsoft.Json;

namespace UniPushSdk.Models.Requests
{
    /// <summary>
    /// 批量推送消息参数
    /// </summary>
    public class BatchPushRequest : BasePushRequest
    {
        /// <summary>
        /// 使用创建消息接口返回的taskId，可以多次使用
        /// </summary>
        [JsonProperty("taskid")]
        public string TaskId { get; set; }

        /// <summary>
        /// 是否异步推送，true是异步，false同步。异步推送不会返回data详情
        /// </summary>
        [JsonProperty("is_async")]
        public bool IsAsync { get; set; }
    }
}
