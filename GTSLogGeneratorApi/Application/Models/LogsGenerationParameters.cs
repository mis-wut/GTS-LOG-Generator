using System.Collections.Generic;
using System.Linq;

namespace GTSLogGeneratorApi.Application.Models
{
    public class LogsGenerationParameters
    {
        public bool IsActive { get; set; }
        
        public int Interval { get; set; }
        
        public string Path { get; set; }

        public int LogsCount { get; set; }

        public List<string> Cities { get; set; } = new List<string>();
        
        public List<string> Channels { get; set; } = new List<string>();

        public List<string> Providers { get; set; } = new List<string>();

        public LogsGenerationParameters Clone()
        {
            return new LogsGenerationParameters()
            {
                IsActive = IsActive,
                Channels = Channels.ToList(),
                Cities = Cities.ToList(),
                Interval = Interval,
                Path = Path,
                Providers = Providers.ToList(),
                LogsCount = LogsCount
            };
        }
    }
}
