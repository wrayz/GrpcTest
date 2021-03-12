using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace GrpcTMRobotServiceClient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();
                var servicesProvider = BuildDi(config);

                using (servicesProvider as IDisposable)
                {
                    var client = servicesProvider.GetRequiredService<TMRobotGrpcClient>();
                    //取得手臂
                    await client.GetTMRobotAsync();
                    //取得手臂 by ip
                    await client.GetTMRobotsByIPAsync("192.168.133.65", "192.168.132.175");
                    //取得手臂Details
                    await client.GetTMRobotDetailsAsync("TM003639", "TM123456");
                    //取得手臂Details by IP
                    await client.GetTMRobotDetailsByIPAsync("192.168.133.65", "192.168.132.175");
                    //取得手臂變數清單
                    // await client.GetTMRobotVariablesAsync("TM003639", "ProjectName", "va1", "v2");
                    //取得手臂變數清單的值
                    // await client.GetTMRobotVariableValueAsync("TM003639", "ProjectName", "v1", "v2");
                    //取得API的手臂控制權
                    // await client.GetTMRobotControlAsync("TM003639", "TM123456");
                    //釋放API的手臂控制權
                    // await client.ReleaseTMRobotControlAsync("TM003639", "TM123456");

                    //改變手臂專案
                    // await client.ChangeRobotDefaultProjectAsync(() =>
                    // {
                    //     var dic = new Dictionary<string, string>();
                    //     dic.Add("k", "v");
                    //     return dic;
                    // });

                    //切換手臂執行
                    // await client.ChangeRobotExecutionAsync(() =>
                    // {
                    //     var dic = new Dictionary<string, TMRobotExcution>();
                    //     dic.Add("k", TMRobotExcution.Play);
                    //     return dic;
                    // });

                    //設定手臂變數值
                    // client.SetRobotVariable("TM123456", "ProjectName", "var1", "123");
                    //拉手臂特定專案
                    // await client.PullProjectAsync("TM123456", "ProjectName");
                    //下載手臂特定專案
                    // client.DownloadProject("TM123456", "ProjectName");
                    //推送手臂特定專案
                    // await client.PushProjectAsync("TM123456", "ProjectName");
                    //上傳手臂特定專案
                    // await client.UploadProject("filename", 999, 3, "encode", new Google.Protobuf.ByteString(), "chacksum");

                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<TMRobotGrpcClient>() // Runner is the custom class
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }
    }
}
