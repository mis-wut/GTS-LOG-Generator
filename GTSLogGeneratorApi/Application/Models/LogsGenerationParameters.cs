using System.Collections.Generic;

namespace GTSLogGeneratorApi.Application.Models
{
    public class LogsGenerationParameters
    {
        public bool IsActive { get; set; }
        
        public int Interval { get; set; }
        
        public string Path { get; set; }

        public int LogsCount { get; set; }

        public List<string> Cities { get; set; }
        
        public List<string> Channels { get; set; }

        public List<string> Providers { get; set; }
    }
}
