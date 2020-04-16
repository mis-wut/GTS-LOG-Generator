using System.Collections.Generic;
using GTSLogGeneratorApi.Application.Models;
using GTSLogGeneratorApi.Infrastructure.Extensions;

namespace GTSLogGeneratorApi.Application.UpdateLogsGenerationJobRequest
{
    public interface ILogsGenerationJobParametersUpdater
    {
        LogsGenerationParameters Update(UpdateLogsGenerationJobRequest source);
    }
    
    public class LogsGenerationJobParametersUpdater : ILogsGenerationJobParametersUpdater
    {
        private readonly LogsGenerationParameters _parameters;

        private readonly List<string> _channels = new List<string>
        {
            "besoin", "laxative", "gnattier", "tensionless",
            "annotate", "emeraldine", "unsoiledness", "membranosis", "took", "giganticness"
        };

        private readonly List<string> _providers = new List<string> {"dsaas", "abcds", "agagr", "gagdd", "reraa"};

        private readonly List<string> _cityIps = new List<string>
        {
            "1.0.106.214", "50.84.238.156", "170.245.98.1", "45.137.219.51", "98.139.252.202",
            "157.51.237.177", "47.49.132.112", "185.131.54.102", "188.26.89.128", "77.152.184.200"
        };

        public LogsGenerationJobParametersUpdater(LogsGenerationParameters parameters)
        {
            _parameters = parameters;
        }

        public LogsGenerationParameters Update(UpdateLogsGenerationJobRequest source)
        {
            var path = source.Path.EndsWith("/") ? source.Path : $"{source.Path}/";
            _parameters.IsActive = source.IsActive;
            _parameters.Interval = source.Interval;
            _parameters.Channels = _channels.GetRandom(source.ChannelsCount);
            _parameters.Providers = _providers.GetRandom(source.ProvidersCount);
            _parameters.Cities = _cityIps.GetRandom(source.CitiesCount);
            _parameters.LogsCount = source.LogsCount;
            _parameters.Path = path;

            return _parameters.Clone();
        }
    }
}