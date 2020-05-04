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
                            var bytesSent = 1000;

                            file.WriteLine(
                                $"\"[22/Mar/2019:14:36:47 +0100]\" \"{timestamp}\" \"{provider}\" \"{upstreamFqdn}\"      \"{serverAddr}\" \"{hostname}\" \"9890462\" \"6\" \"212.160.172.71:51293\" \"PL\"        \"https\" \"GET\" \"/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-audio_198800_pol=196800-74556566476625.dash?bpk-service=LIVE&dt=0\" \"OK\" \"0.026\"     \"{httpCode}\" \"{bytesSent}\" \"47561\" \"video/mp4\" \"47561\" \"-\" \"-\"       \"MISS\" \"MISS\" \"MISS\"    \"192.168.80.102:80\" \"200\" \"0.000\" \"0.025\" \"47561\"  \"c23.default.ocdn.rd.tp.pl\" \"Mozilla/5.0 (Windows NT 10.0; WOW64; rv:60.0) Gecko/20100101 Firefox/60.0\" \"http://orange-opensource.github.io\" \"http://orange-opensource.github.io/hasplayer.js/1.15.1/samples/Dash-IF/index.html\" \"-\" \"keep-alive\" \"HTTP/1.1\"       \"on\" \"TLSv1.2\" \"c23.default.ocdn.rd.tp.pl\" \".\" \"\"       \"-\" \"-\"         \"38\" \"0\" \"34\" \"3\" \"23445\"       \"6375\" \"5000\" \"10\" \"14480\"");
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