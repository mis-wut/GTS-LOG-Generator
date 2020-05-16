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

        public List<string> Providers { get; set; } = new List<string>();

        public List<string> ServerAddresses { get; set; } = new List<string>();

        public List<string> UpstreamFqdns { get; set; } = new List<string>();

        public List<string> Hostnames { get; set; } = new List<string>();

        public List<string> HttpCodes { get; set; } = new List<string>();

        public List<string> Communities { get; set; } = new List<string>();

        public List<string> UserAgents { get; set; } = new List<string>();

        public List<string> RequestUris { get; set; } = new List<string>();

        public LogsGenerationParameters Clone()
        {
            return new LogsGenerationParameters()
            {
                IsActive = IsActive,
                Providers = Providers.ToList(),
                Hostnames = Hostnames.ToList(),
                ServerAddresses = ServerAddresses.ToList(),
                UpstreamFqdns = UpstreamFqdns.ToList(),
                HttpCodes = HttpCodes.ToList(),
                Communities = Communities.ToList(),
                UserAgents = UserAgents.ToList(),
                RequestUris = RequestUris.ToList(),
                Interval = Interval,
                Path = Path,
                LogsCount = LogsCount
            };
        }
    }
}
