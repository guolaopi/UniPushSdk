using Newtonsoft.Json;

namespace UniPushSdk.Models
{
    /// <summary>
    /// 个推通道消息内容
    /// </summary>
    public class PushMessageModel
    {
        /// <summary>
        /// 手机端通知展示时间段，格式为毫秒时间戳段，两个时间的时间差必须大于10分钟，例如："1590547347000-1590633747000"
        /// </summary>
        [JsonProperty("duration")]
        public string Duration { get; set; }

        /// <summary>
        /// 纯透传消息内容，安卓和iOS均支持，与notification、revoke 三选一，都填写时报错，长度 ≤ 3072
        /// </summary>
        [JsonProperty("transmission")]
        public string Transmission { get; set; }

        /// <summary>
        /// 通知消息内容，仅支持安卓系统，iOS系统不展示个推通知消息，与transmission、revoke三选一，都填写时报错
        /// </summary>
        [JsonProperty("notification")]
        public Notification Notification { get; set; }
    }

    /// <summary>
    /// 通知消息内容
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// 通知消息标题，长度 ≤ 50
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 通知消息内容，长度 ≤ 256
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// intent：打开应用内特定页面，
        /// url：打开网页地址，
        /// payload：自定义消息内容启动应用，
        /// payload_custom：自定义消息内容不启动应用，
        /// startapp：打开应用首页，
        /// none：纯通知，无后续动作
        /// </summary>
        [JsonProperty("click_type")]
        public string ClickType { get; set; }

        /// <summary>
        /// click_type为intent时必填,点击通知打开应用特定页面，长度 ≤ 4096;示例：intent:#Intent;component=你的包名/你要打开的 activity 全路径;S.parm1=value1;S.parm2=value2;end
        /// </summary>
        [JsonProperty("intent")]
        public string Intent { get; set; }

        /// <summary>
        /// click_type为url时必填,点击通知打开链接，长度 ≤ 1024
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// click_type为payload/payload_custom时必填,点击通知加自定义消息，长度 ≤ 3072
        /// </summary>
        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}
