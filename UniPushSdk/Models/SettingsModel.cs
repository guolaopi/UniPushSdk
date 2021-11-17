using Newtonsoft.Json;
using System.Collections.Generic;

namespace UniPushSdk.Models
{
    /// <summary>
    /// 推送条件设置
    /// </summary>
    public class SettingsModel
    {
        /// <summary>
        /// 消息离线时间设置，单位毫秒，-1表示不设离线，-1 ～ 3 * 24 * 3600 * 1000(3天)之间 默认值1小时
        /// </summary>
        [JsonProperty("ttl")]
        public int TTL { get; set; }

        /// <summary>
        /// 厂商通道策略 默认值{"strategy":{"default":1}}
        /// </summary>
        /*
         * 此项为一个字典，key为uniPush支持的厂商编码，
         * key的可选值为：default、ios、st、hw、xm、vv、mz、op
         * value为：1、2、3、4
         * 1: 表示该消息在用户在线时推送个推通道，用户离线时推送厂商通道;
         * 2: 表示该消息只通过厂商通道策略下发，不考虑用户是否在线;
         * 3: 表示该消息只通过个推通道下发，不考虑用户是否在线；
         * 4: 表示该消息优先从厂商通道下发，若消息内容在厂商通道代发失败后会从个推通道下发。
         * 
         * **注意：要推送ios通道，需要在个推开发者中心上传ios证书，建议填写2或4，否则可能会有消息不展示的问题
         */
        [JsonProperty("strategy")]
        public Dictionary<string, int> Strategy { get; set; }
    }
}
