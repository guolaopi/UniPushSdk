using UniPushSdk.Enums;

namespace UniPushSdk.Models
{
    /// <summary>
    /// 推送消息的Model
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 点击后执行的操作类型
        /// </summary>
        public ClickType ClickType { get; set; }

        /// <summary>
        /// 点击后执行的操作内容
        /// </summary>
        public string ClickObj { get; set; }
    }
}
