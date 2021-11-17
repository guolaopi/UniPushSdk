using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniPushSdk;
using UniPushSdk.Enums;
using UniPushSdk.Models;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting push......");

            var pusher = new UniPushService(new Config
            {
                AppId = "AppId",
                AppKey = "AppKey",
                AppSecret = "AppSecret",
                MasterSecret = "MasterSecret",
            });

            // 群推送一条消息给所有用户，点击后会打开app
            await pusher.AllPush(new MessageModel
            {
                Title = "群推标题 - 来自C#",
                Content = "这是一条来自C#发送的群推测试消息，所有用户都可以收到",
                ClickType = ClickType.startapp,
            });

            // 单独推送给cid为123456的用户一条消息，点击后会调用系统浏览器打开https://www.baidu.com
            await pusher.SinglePush("123456",
                                    new MessageModel
                                    {
                                        Title = "单独推送标题-来自C#",
                                        Content = "这是一条来自C#发送的测试消息，单独推送给123456",
                                        ClickType = ClickType.url,
                                        ClickObj = "https://www.baidu.com"
                                    });

            // 批量给cid为11111、222222、333333的用户推送消息，点击后会调用系统浏览器打开https://www.baidu.com
            await pusher.BatchPush(new List<string> { "111111", "222222", "333333" },
                                   new MessageModel
                                   {
                                       Title = "批量推送标题-来自C#",
                                       Content = "这是一条来自C#发送的批量推送的测试消息",
                                       ClickType = ClickType.url,
                                       ClickObj = "https://www.baidu.com"
                                   });

            Console.WriteLine("end......");
            Console.ReadLine();
        }
    }
}
