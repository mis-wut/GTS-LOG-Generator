using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GTSLogGeneratorApi.Jobs
{
    public class LogsGenerationJob : ILogsGenerationJob
    {
        public static string Id = "LogsGenerationJob";


        public Task Execute(LogsGenerationParameters parameters)
        {
            // TODO: Generowanie pliku
            return Task.CompletedTask;
        }
    }

    public interface ILogsGenerationJob
    {
        Task Execute(LogsGenerationParameters parameters);
    }
}