using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GTSLogGeneratorApi.Extensions;

namespace GTSLogGeneratorApi.Jobs
{
    public class LogsGenerationJob : ILogsGenerationJob
    {
        public static string Id = "LogsGenerationJob";
        

        public Task Execute(LogsGenerationParameters parameters)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            using (StreamWriter file = new StreamWriter($"{parameters.Path}/{timestamp}.log"))
            {
                for (var i = 1; i <= parameters.LogsNumber; i++)
                {
                    var channel = parameters.Channels.GetRandomElement(i);
                    var city = parameters.Cities.GetRandomElement(i);
                    var provider = parameters.Providers.GetRandomElement(i);
                    
                    file.WriteLine($"\"2020-02-10T17:15:58+02:00\"\"90.84.143.49\"\"nginx:\"\"{city}\"\"-\"\"[10/Feb/2020:17:15:58 +0000]\"\"GET\"\"http://test.com/myvideo/download/{provider}/{channel}//\"\"HTTP/1.1\"\"200\"\"1000\"\"AffxVbxfwindowsxdCAECv\"\"{timestamp}");
                }
            }
            
            return Task.CompletedTask;
        }
    }

    public interface ILogsGenerationJob
    {
        Task Execute(LogsGenerationParameters parameters);
    }
}