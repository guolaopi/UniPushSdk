using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UniPushSdk
{
    /// <summary>
    /// Http请求类
    /// </summary>
    internal static class HttpTool
    {
        private static string _ContentType = "application/json";
        private static string _Charset = "utf-8";

        /// <summary>
        /// post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static async Task<T> Post<T>(string url, string json, Dictionary<string, string> headers = null)
        {
            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) })
            {
                var content = new StringContent(json, Encoding.GetEncoding(_Charset)) as HttpContent;
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(_ContentType);

                if (headers != null)
                {
                    foreach (var kv in headers)
                    {
                        content.Headers.Add(kv.Key, kv.Value);
                    }
                }

                var resopense = await client.PostAsync(url, content);
                var resultStr = await resopense.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(resultStr);

                return result;
            }
        }
    }
}
