using System.Collections.Generic;
using GTSLogGeneratorApi.Controllers;
using GTSLogGeneratorApi.Extensions;
using GTSLogGeneratorApi.Jobs;

namespace GTSLogGeneratorApi.Mappers
{
    public interface ILogsGenerationParametersMapper
    {
        LogsGenerationParameters Map(UpdateLogsGenerationJobRequest source);
    }

    public class LogsGenerationParametersMapper : ILogsGenerationParametersMapper
    {
        private readonly List<string> Channels = new List<string>
        {
            "besoin", "laxative", "gnattier", "tensionless",
            "annotate", "emeraldine", "unsoiledness", "membranosis", "took", "giganticness"
        };

        private readonly List<string> Providers = new List<string> {"dsaas", "abcds", "agagr", "gagdd", "reraa"};

        private readonly List<string> CitiesNumber = new List<string>
        {
            "1.0.106.214", "50.84.238.156", "170.245.98.1", "45.137.219.51", "98.139.252.202",
            "157.51.237.177", "47.49.132.112", "185.131.54.102", "188.26.89.128", "77.152.184.200"
        };

        public LogsGenerationParameters Map(UpdateLogsGenerationJobRequest source)
        {
            var path = source.Path.EndsWith("/") ? source.Path : $"{source.Path}/";
            return new LogsGenerationParameters
            {
                IsActive = source.IsActive,
                Interval = source.Interval,
                Channels = Channels.GetRandom(source.ChannelsCount),
                Providers = Providers.GetRandom(source.ProvidersCount),
                Cities = CitiesNumber.GetRandom(source.CitiesCount),
                LogsCount = source.LogsCount,
                Path = path
            };
        }
    }
}