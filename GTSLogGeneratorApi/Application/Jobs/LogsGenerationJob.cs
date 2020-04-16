using System;
using System.IO;
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
        public void Execute(LogsGenerationParameters parameters)
        {
            if(Directory.GetFiles(parameters.Path).Length >= 20)
                return;
            
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            using (StreamWriter file = new StreamWriter($"{parameters.Path}/{timestamp}.log"))
            {
                for (var i = 1; i <= parameters.LogsCount; i++)
                {
                    var channel = parameters.Channels.GetRandomElement();
                    var city = parameters.Cities.GetRandomElement();
                    var provider = parameters.Providers.GetRandomElement();
                    
                    file.WriteLine($"\"2020-02-10T17:15:58+02:00\"\"90.84.143.49\"\"nginx:\"\"{city}\"\"-\"\"[10/Feb/2020:17:15:58 +0000]\"\"GET\"\"http://test.com/myvideo/download/{channel}/{provider}//\"\"HTTP/1.1\"\"200\"\"1000\"\"AffxVbxfwindowsxdCAECv\"\"{timestamp}\"");
                }
            }
        }
    }

    public interface ILogsGenerationJob
    {
        void Execute(LogsGenerationParameters parameters);
    }
}