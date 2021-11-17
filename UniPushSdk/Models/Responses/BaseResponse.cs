using Newtonsoft.Json;

namespace UniPushSdk.Models.Responses
{
    /// <summary>
    /// 公共返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
