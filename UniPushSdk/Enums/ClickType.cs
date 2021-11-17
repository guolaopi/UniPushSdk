namespace UniPushSdk.Enums
{
    /// <summary>
    /// 点击消息后的操作类型
    /// </summary>
    public enum ClickType
    {
        /// <summary>
        /// 打开应用内特定页面
        /// </summary>
        intent,

        /// <summary>
        /// 打开网页地址
        /// </summary>
        url,

        /// <summary>
        /// 自定义消息内容启动应用
        /// </summary>
        payload,

        /// <summary>
        /// 自定义消息内容不启动应用
        /// </summary>
        payload_custom,

        /// <summary>
        /// 打开应用首页
        /// </summary>
        startapp,

        /// <summary>
        /// 纯通知，无后续动作
        /// </summary>
        none,
    }
}
