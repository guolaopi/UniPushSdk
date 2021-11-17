using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniPushSdk.Models.Requests
{
    /// <summary>
    /// 创建消息基类
    /// </summary>
    public class CreateMessageRequest
    {
        /// <summary>
        /// 请求唯一标识号，10-32位之间；如果request_id重复，会导致消息丢失
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// 推送条件设置
        /// </summary>
        //[JsonProperty("settings")]
        //public string Settings { get; set; }

        /// <summary>
        /// 个推推送消息参数
        /// </summary>
        [JsonProperty("push_message")]
        public PushMessageModel PushMessage { get; set; }
    }
}
