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
        private readonly ILogger<LogsGenerationJob> _logger;
        public static string Id = "LogsGenerationJob";

        public static LogsGenerationParameters Parameters = new LogsGenerationParameters();

        public LogsGenerationJob(ILogger<LogsGenerationJob> logger)
        {
            _logger = logger;
        }
        
        public async Task Execute()
        {
            while (true)
            {
                try
                {
                    if (Parameters == null ||
                        !Parameters.IsActive ||
                        !Directory.Exists(Parameters.Path) ||
                        Directory.GetFiles(Parameters.Path).Length >= 20)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(200));
                        continue;
                    }

                    var parameters = Parameters.Clone();
                    var startGeneration = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    _logger.LogInformation($"[{DateTime.Now.ToString("hh:mm:ss:fff")}] Start generating file: {timestamp}.log ");
                    using (StreamWriter file = new StreamWriter($"tmp_{timestamp}.log"))
                    {
                        for (var i = 1; i <= parameters.LogsCount; i++)
                        {
                            var hostname = parameters.Hostnames.GetRandomElement();
                            var serverAddr = parameters.ServerAddresses.GetRandomElement();
                            var upstreamFqdn = parameters.UpstreamFqdns.GetRandomElement();
                            var provider = parameters.Providers.GetRandomElement();
                            var httpCode = parameters.HttpCodes.GetRandomElement();
                            var community = parameters.Communities.GetRandomElement();
                            var upstreamRequestStatus = new Random().NextDouble() <= 0.1 ? "MISS" : "HIT";
                            var userAgent = parameters.UserAgents.GetRandomElement();
                            var requestUri = parameters.RequestUris.GetRandomElement();
                            var bytesSent = 1000;

                            file.WriteLine(
                                $"\"[22/Mar/2019:14:36:47 +0100]\" \"{timestamp}\" \"{provider}\" \"{upstreamFqdn}\"      \"{serverAddr}\" \"{hostname}\" \"9890462\" \"6\" \"{community}:80\" \"PL\"        \"https\" \"GET\" \"{requestUri}\" \"OK\" \"0.026\"     \"{httpCode}\" \"{bytesSent}\" \"47561\" \"video/mp4\" \"47561\" \"-\" \"-\"       \"{upstreamRequestStatus}\" \"{upstreamRequestStatus}\" \"{upstreamRequestStatus}\"    \"192.168.80.102:80\" \"200\" \"0.000\" \"0.025\" \"47561\"  \"c23.default.ocdn.rd.tp.pl\" \"{userAgent}\" \"http://orange-opensource.github.io\" \"http://orange-opensource.github.io/hasplayer.js/1.15.1/samples/Dash-IF/index.html\" \"-\" \"keep-alive\" \"HTTP/1.1\"       \"on\" \"TLSv1.2\" \"c23.default.ocdn.rd.tp.pl\" \".\" \"\"       \"-\" \"-\"         \"38\" \"0\" \"34\" \"3\" \"23445\"       \"6375\" \"5000\" \"10\" \"14480\"");
                        }
                    }
                    File.Move($"tmp_{timestamp}.log", Path.Combine(parameters.Path, $"{timestamp}.log"));

                    var endGeneration = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    _logger.LogInformation($"[{DateTime.Now.ToString("hh:mm:ss:fff")}] End generating file: {timestamp}.log ");
                    var differenceMs = parameters.Interval * 1000 - (endGeneration - startGeneration);
                    if (differenceMs > 0)
                    {
                        _logger.LogInformation($"Sleeping time: {differenceMs}");
                        await Task.Delay(TimeSpan.FromMilliseconds(differenceMs));
                    }
                    _logger.LogInformation($"Job executiom time: {endGeneration - startGeneration}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[LogsGenerationJob]: {ex.Message}");
                }
            }
        }
    }

    public interface ILogsGenerationJob
    {
        Task Execute();
    }
}