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
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                        return;

                    if (!Directory.Exists(parameters.Path) ||
                        Directory.GetFiles(parameters.Path).Length >= 20)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(200));
                        continue;
                    }

                    var startGeneration = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    
                    using (StreamWriter file = new StreamWriter($"tmp_{timestamp}.log"))
                    {
                        for (var i = 1; i <= parameters.LogsCount; i++)
                        {
                            var channel = parameters.Channels.GetRandomElement();
                            var city = parameters.Cities.GetRandomElement();
                            var provider = parameters.Providers.GetRandomElement();

                            file.WriteLine(
                                $"\"2020-02-10T17:15:58+02:00\"\"90.84.143.49\"\"nginx:\"\"{city}\"\"-\"\"[10/Feb/2020:17:15:58 +0000]\"\"GET\"\"http://test.com/myvideo/download/{channel}/{provider}//\"\"HTTP/1.1\"\"200\"\"1000\"\"AffxVbxfwindowsxdCAECv\"\"{timestamp}\"");
                        }

                    }
                    File.Move($"tmp_{timestamp}.log", Path.Combine(parameters.Path, $"{timestamp}.log"));

                    var endGeneration = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    var differenceMs = parameters.Interval * 1000 - (endGeneration - startGeneration);
                    if (differenceMs > 0)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(differenceMs));
                    }
                    timestamp += parameters.Interval;
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