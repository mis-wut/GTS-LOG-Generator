using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTSLogGeneratorApi.Jobs
{
    public class LogsGenerationParameters
    {
        public bool IsActive { get; set; }

        public int Interval { get; set; }

        public int LogsNumber { get; set; }

        public int ChannelNumber { get; set; }

        public int ProvidersNumber { get; set; }
    }
}
