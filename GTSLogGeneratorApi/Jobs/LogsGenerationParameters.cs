using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTSLogGeneratorApi.Jobs
{
    public class LogsGenerationParameters
    {
        public string Path { get; set; }

        public int LogsNumber { get; set; }

        public List<string> Cities { get; set; }
        
        public List<string> Channels { get; set; }

        public List<string> Providers { get; set; }
    }
}
