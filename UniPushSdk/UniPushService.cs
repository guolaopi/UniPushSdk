using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniPushSdk.Enums;
using UniPushSdk.Models;
using UniPushSdk.Models.Requests;
using UniPushSdk.Models.Responses;

namespace UniPushSdk
{
    /// <summary>
    /// 推送消息服务类
    /// </summary>
    public class UniPushService
    {
        /// <summary>
        /// 配置
        /// </summary>
        private Config _Config;

        /// <summary>
        /// 接口前缀
        /// </summary>
        private string _ApiBaseUrl = "";

        public UniPushService(Config config)
        {
            if (string.IsNullOrEmpty(config.AppId))
            {
                throw new ArgumentNullException("AppId不能为空");
            }
            if (string.IsNullOrEmpty(config.AppKey))
            {
                throw new ArgumentNullException("AppKey不能为空");
            }
            if (string.IsNullOrEmpty(config.AppSecret))
            {
                throw new ArgumentNullException("AppSecret不能为空");
            }
            if (string.IsNullOrEmpty(config.MasterSecret))
            {
                throw new ArgumentNullException("MasterSecret不能为空");
            }
            _Config = config;
            _ApiBaseUrl = $"https://restapi.getui.com/v2/{config.AppId}";
        }

        #region 私有方法

        /// <summary>
        /// 获取32位guid
        /// </summary>
        /// <returns></returns>
        private string GetGuid() => Guid.NewGuid().ToString().Replace("-", "");

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string SHA256EncryptString(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        private async Task<BaseResponse<TokenResponse>> GetToken()
        {
            var url = $"{_ApiBaseUrl}/auth";
            var ts = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var sign = SHA256EncryptString($"{_Config.AppKey}{ts}{_Config.MasterSecret}");
            var body = new TokenRequest { AppKey = _Config.AppKey, Timestamp = ts.ToString(), Sign = sign };
            var result = await HttpTool.Post<BaseResponse<TokenResponse>>(url, JsonConvert.SerializeObject(body));
            return result;
        }

        /// <summary>
        /// 创建消息
        /// </summary>
        /// <returns></returns>
        private async Task<BaseResponse<CreateMessageResponse>> CreateMessage(MessageModel msg)
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token.Data.Token))
            {
                return new BaseResponse<CreateMessageResponse>
                {
                    Code = token.Code,
                    Msg = token.Msg,
                };
            }

            var url = $"{_ApiBaseUrl}/auth";

            var request = new CreateMessageRequest
            {
                RequestId = GetGuid(),
                PushMessage = new PushMessageModel
                {
                    Notification = new Notification
                    {
                        Title = msg.Title,
                        Body = msg.Content,
                        ClickType = msg.ClickType.ToString(),
                    }
                }
            };

            switch (msg.ClickType)
            {
                case ClickType.intent:
                    request.PushMessage.Notification.Intent = msg.ClickObj;
                    break;
                case ClickType.url:
                    request.PushMessage.Notification.Url = msg.ClickObj;
                    break;
                case ClickType.payload:
                    request.PushMessage.Notification.Payload = msg.ClickObj;
                    break;
                case ClickType.payload_custom:
                    request.PushMessage.Notification.Payload = msg.ClickObj;
                    break;
                default:
                    break;
            }

            var result = await HttpTool.Post<BaseResponse<CreateMessageResponse>>(url,
                                                                                  JsonConvert.SerializeObject(request),
                                                                                  new Dictionary<string, string> { { "token", token.Data.Token } });
            return result;
        }

        #endregion

        /// <summary>
        /// 全体推送
        /// </summary>
        /// <param name="msg">消息内容，注意content不能重复，否则会推送失败</param>
        /// <param name="settings">推送设置</param>
        /// <returns></returns>
        public async Task<BaseResponse<PushResponse>> AllPush(MessageModel msg, SettingsModel settings = null)
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token.Data.Token))
            {
                return new BaseResponse<PushResponse>
                {
                    Code = token.Code,
                    Msg = token.Msg,
                };
            }

            var request = new AllPushRequest
            {
                RequestId = GetGuid(),
                PushMessage = new PushMessageModel
                {
                    Notification = new Notification
                    {
                        Title = msg.Title,
                        Body = msg.Content,
                        ClickType = msg.ClickType.ToString(),
                    }
                }
            };
            switch (msg.ClickType)
            {
                case ClickType.intent:
                    request.PushMessage.Notification.Intent = msg.ClickObj;
                    break;
                case ClickType.url:
                    request.PushMessage.Notification.Url = msg.ClickObj;
                    break;
                case ClickType.payload:
                    request.PushMessage.Notification.Payload = msg.ClickObj;
                    break;
                case ClickType.payload_custom:
                    request.PushMessage.Notification.Payload = msg.ClickObj;
                    break;
                default:
                    break;
            }
            if (settings != null)
            {
                request.Settings = settings;
            }

            var url = $"{_ApiBaseUrl}/push/all";
            var result = await HttpTool.Post<BaseResponse<PushResponse>>(url,
                                                                         JsonConvert.SerializeObject(request),
                                                                         new Dictionary<string, string> { { "token", token.Data.Token } });
            return result;
        }

        /// <summary>
        /// 推送单一消息
        /// </summary>
        /// <param name="cid">指定用户的cid</param>
        /// <param name="msg">消息内容，注意content不能重复，否则会推送失败</param>
        /// <param name="settings">推送设置</param>
        /// <returns></returns>
        public async Task<BaseResponse<PushResponse>> SinglePush(string cid, MessageModel msg, SettingsModel settings = null)
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token.Data.Token))
            {

                return new BaseResponse<PushResponse>
                {
                    Code = token.Code,
                    Msg = token.Msg,
                };
            }

            var request = new SinglePushRequest
            {
                RequestId = GetGuid(),
                Audience = new AudienceModel { CId = new List<string> { cid } },
                PushMessage = new PushMessageModel
                {
                    Notification = new Notification
                    {
                        Title = msg.Title,
                        Body = msg.Content,
                        ClickType = msg.ClickType.ToString(),
                    }
                }
            };
            switch (msg.ClickType)
            {
                case ClickType.intent:
                    request.PushMessage.Notification.Intent = msg.ClickObj;
                    break;
                case ClickType.url:
                    request.PushMessage.Notification.Url = msg.ClickObj;
                    break;
                case ClickType.payload:
                    request.PushMessage.Notification.Payload = msg.ClickObj;
                    break;
                case ClickType.payload_custom:
                    request.PushMessage.Notification.Payload = msg.ClickObj;
                    break;
                default:
                    break;
            }
            if (settings != null)
            {
                request.Settings = settings;
            }

            var url = $"{_ApiBaseUrl}/push/single/cid";
            var result = await HttpTool.Post<BaseResponse<PushResponse>>(url,
                                                                         JsonConvert.SerializeObject(request),
                                                                         new Dictionary<string, string> { { "token", token.Data.Token } });
            return result;
        }

        /// <summary>
        /// 批量推送
        /// </summary>
        /// <param name="cids">批量推送用户的cid</param>
        /// <param name="msg">消息内容，注意content不能重复，否则会推送失败</param>
        /// <param name="isAsync">是否异步推送，如果异步推送的话无法直接获取推送结果</param>
        /// <param name="settings">推送设置</param>
        /// <returns></returns>
        public async Task<BaseResponse<PushResponse>> BatchPush(List<string> cids, MessageModel msg, bool isAsync = false, SettingsModel settings = null)
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token.Data.Token))
            {

                return new BaseResponse<PushResponse>
                {
                    Code = token.Code,
                    Msg = token.Msg,
                };
            }

            var msgResult = await CreateMessage(msg);
            if (msgResult == null || string.IsNullOrEmpty(msgResult.Data.TaskId))
            {
                return null;
            }

            var request = new BatchPushRequest
            {
                RequestId = GetGuid(),
                Audience = new AudienceModel { CId = cids },
                TaskId = msgResult.Data.TaskId,
                IsAsync = false, // 如果异步推送的话无法直接获取推送结果
            };
            if (settings != null)
            {
                request.Settings = settings;
            }

            var url = $"{_ApiBaseUrl}/push/list/cid";
            var result = await HttpTool.Post<BaseResponse<PushResponse>>(url,
                                                                         JsonConvert.SerializeObject(request),
                                                                         new Dictionary<string, string> { { "token", token.Data.Token } });
            return result;
        }
    }
}
