using System;
using System.Collections.Generic;
using System.Linq;
using GTSLogGeneratorApi.Application.Jobs;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Extensions;

namespace GTSLogGeneratorApi.Application.RunLogsGenerationJobRequest
{
    public interface ILogsGenerationJobParametersMapper
    {
        LogsGenerationParameters Map(RunLogsGenerationJobRequest source);
    }
    
    public class LogsGenerationJobParametersMapper : ILogsGenerationJobParametersMapper
    {
        private static readonly HashSet<string> _hostnames = new HashSet<string>
        {
            "besoin", "laxative", "gnattier", "tensionless", "annotate", "any", "tail", "keyboard", "ordinary", "hostname",
            "cupoftea", "bottle", "hgawqet", "masfhqwe", "makeq", "mogqhweu", "qwertyq", "loktpeta", "asdwqe", "zmawuehq"
        };

        private static readonly HashSet<string> _providers = new HashSet<string>
        {
            "dsaas", "abcds", "agagr", "gagdd", "reraa",
            "efgh", "jkloap", "mkrto", "azamqw", "xasdg"
        };

        private static readonly HashSet<string> _contentClusters = new HashSet<string>()
        {
            "c10.default.ocdn.rd.tp.pl",
            "c11.default.ocdn.rd.tp.pl",
            "c12.default.ocdn.rd.tp.pl",
            "c13.default.ocdn.rd.tp.pl",
            "c14.default.ocdn.rd.tp.pl",
            "c15.default.ocdn.rd.tp.pl",
            "c16.default.ocdn.rd.tp.pl",
            "c17.default.ocdn.rd.tp.pl",
            "c18.default.ocdn.rd.tp.pl",
            "c19.default.ocdn.rd.tp.pl",
            "c20.default.ocdn.rd.tp.pl",
            "c21.default.ocdn.rd.tp.pl",
            "c22.default.ocdn.rd.tp.pl",
            "c23.default.ocdn.rd.tp.pl",
            "c24.default.ocdn.rd.tp.pl",
            "c25.default.ocdn.rd.tp.pl",
            "c26.default.ocdn.rd.tp.pl",
            "c27.default.ocdn.rd.tp.pl",
            "c28.default.ocdn.rd.tp.pl",
            "c29.default.ocdn.rd.tp.pl"
        };
        
        private static readonly HashSet<string> _serverAddresses = GetRandomIpAddresses(120);

        private static readonly HashSet<string> _upstreamFqdns = GetRandomIpAddresses(20);
        
        private static readonly HashSet<string> _communties = GetRandomIpAddresses(20);

        private static readonly HashSet<string> _httpCodes = new HashSet<string>
        {
            "200", "201", "202", "203", "204", "300", "301", "302", "303", "304",
            "400", "401", "404", "405", "407", "500"
        };

        private static readonly HashSet<string> _userAgents = new HashSet<string>()
        {
            "Mozilla 1.0", "Mozilla 2.0", "Mozilla 3.0", "VO Player", 
            "Opera 1.0", "Opera 2.0", "Opera 3.0", "Safari 1.0", "Safari 2.0", "Android", "iOS", 
            "IE 4", "IE 5", "MS Edge", "Chromium 1.0", "Chromium 2.0", "Chromium 3.0",
            "Chrome 1.0", "Chrome 2.0", "Chrome 3.0"
        };

        private static readonly HashSet<string> _requestUris = new HashSet<string>()
        {
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-audio_198800_pol=196800-74556566476625.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/index.mpd?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-video=2395600.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-audio=6233116.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-video=5123651.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-audio_198800_pol=196800-74556566571857.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-video=2395600-931957082148.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/index.mpd?bpk-service2=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-audio_198800_pol=196800-74556566668113.dash?bpk-service=LIVE&dt=0",
            "/otvppd/OTF/token/timestamp/HH_ID/TERM_ID/14204/pool01/bpk-tv/14204/DASH/dash/canalfilm-video=2395600-931957083348.dash?bpk-service=LIVE&dt=0"
        };



        public LogsGenerationParameters Map(RunLogsGenerationJobRequest source)
        {
            var path = source.Path.EndsWith("/") ? source.Path : $"{source.Path}/";
            var parameters = new LogsGenerationParameters
            {
                Interval = source.Interval,
                ContentClusters = _contentClusters.Take(source.ContentClustersCount).ToList(),
                Providers = _providers.Take(source.ProvidersCount).ToList(),
                ServerAddresses = _serverAddresses.Take(source.ServerAddressesCount).ToList(),
                Hostnames = _hostnames.Take(source.HostnamesCount).ToList(),
                UpstreamFqdns = _upstreamFqdns.Take(source.UpstreamFqdnsCount).ToList(),
                HttpCodes = _httpCodes.ToList().GetRandom(source.HttpCodesCount),
                Communities = _communties.Take(source.CommunitiesCount).ToList(),
                UserAgents = _userAgents.ToList(),
                RequestUris = _requestUris.ToList(),
                LogsFilesCount = source.LogsFilesCount,
                LogsCount = source.LogsCount,
                Path = path
            };

            LogsGenerationJob.LastParameters = parameters.Clone();

            return parameters;
        }

        private static HashSet<string> GetRandomIpAddresses(int count)
        {
            var result = new HashSet<string>();

            for(var i = 1; i <= count; i++)
            {
                result.Add($"1.1.1.{i}");
            }

            return result;
        }
    }
}