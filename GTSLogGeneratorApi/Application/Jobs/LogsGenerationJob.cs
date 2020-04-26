using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Extensions;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace GTSLogGeneratorApi.Application.Jobs
{
    public class LogsGenerationJob : ILogsGenerationJob
    {
        public static string Id = "LogsGenerationJob";

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Execute(LogsGenerationParameters parameters, CancellationToken token)
        {

            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                        return;
                    
                    if (!Directory.Exists(parameters.Path) ||
                        Directory.GetFiles(parameters.Path).Length >= 20)
                        return;
                    
                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    var utcNow = DateTimeOffset.UtcNow;
                    var startGeneration = utcNow.ToUnixTimeMilliseconds();
                    
                    var content = new StringBuilder();
                    for (var i = 1; i <= parameters.LogsCount; i++)
                    {
                        var channel = parameters.Channels.GetRandomElement();
                        var city = parameters.Cities.GetRandomElement();
                        var provider = parameters.Providers.GetRandomElement();

                        content.AppendLine(
                            $"\"2020-02-10T17:15:58+02:00\"\"90.84.143.49\"\"nginx:\"\"{city}\"\"-\"\"[10/Feb/2020:17:15:58 +0000]\"\"GET\"\"http://test.com/myvideo/download/{channel}/{provider}//\"\"HTTP/1.1\"\"200\"\"1000\"\"AffxVbxfwindowsxdCAECv\"\"{timestamp}\"");
                    }

                    var targetPath = Path.Combine(parameters.Path, $"{timestamp}.log");
                    if (!File.Exists(targetPath))
                    {
                        File.WriteAllText(Path.Combine(parameters.Path, $"{timestamp}.log"), content.ToString());
                    }
                    var endGeneration = utcNow.AddSeconds(parameters.Interval).ToUnixTimeMilliseconds();
                    var differenceMs = endGeneration - startGeneration;
                    if (differenceMs > 0)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(differenceMs));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LogsGenerationJob]: {ex.Message}");
                }
            }
        }
    }

    public interface ILogsGenerationJob
    {
        Task Execute(LogsGenerationParameters parameters, CancellationToken token);
    }
}