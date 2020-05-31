using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static LogsGenerationParameters LastParameters = new LogsGenerationParameters();

        public LogsGenerationJob(ILogger<LogsGenerationJob> logger)
        {
            _logger = logger;
        }

        public void Execute(LogsGenerationParameters parameters)
        {
            var startGeneration = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var jobDirectory = $"{startGeneration}";
            Directory.CreateDirectory(jobDirectory);

            try
            {
                GenerateLogs(parameters, startGeneration, jobDirectory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured durning logs generation: {ex.Message}");
            }
            finally
            {
                Clean(jobDirectory);
            }
        }

        private static void Clean(string jobDirectory)
        {
            Directory.Delete(jobDirectory, true);
        }

        private void GenerateLogs(LogsGenerationParameters parameters, long startGeneration, string jobDirectory)
        {
            _logger.LogInformation($"Start generating job {startGeneration}.");

            if (!Directory.Exists(parameters.Path))
            {
                throw new InvalidOperationException($"Provided path {parameters.Path} does not exists.");
            }

            GenerateLogsFiles(parameters, startGeneration, jobDirectory);
            MoveLogsFiles(parameters, jobDirectory);
        }

        private void GenerateLogsFiles(LogsGenerationParameters parameters, long startGeneration, string jobDirectory)
        {
            for (var j = 0; j < parameters.LogsFilesCount; j++)
            {
                var timestamp = startGeneration + parameters.Interval * j;
                _logger.LogInformation($"Start generating {timestamp}.log");
                using (StreamWriter file = new StreamWriter(Path.Combine(jobDirectory, $"{timestamp}.log")))
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
            }
        }

        private void MoveLogsFiles(LogsGenerationParameters parameters, string jobDirectory)
        {
            var files = GetFiles(jobDirectory);
            foreach (var filename in files)
            {
                File.Move(Path.Combine(jobDirectory, filename), Path.Combine(parameters.Path, filename));
                Thread.Sleep(TimeSpan.FromSeconds(parameters.Interval));
                _logger.LogInformation($"[{DateTime.Now:hh:mm:ss:fff}] End moving file: {filename}");
            }
        }

        private IEnumerable<string> GetFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            var dirs = (from file in dir.EnumerateFiles()
                orderby file.CreationTime
                select file.Name).Distinct();
            return dirs;
        }
    }

    public interface ILogsGenerationJob
    {
        void Execute(LogsGenerationParameters parameters);
    }
}