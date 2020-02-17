namespace GTSLogGeneratorApi.Controllers
{
    public class UpdateLogsGenerationJobRequest
    {
        public bool IsActive { get; set; }

        public int Interval { get; set; }

        public int LogsNumber { get; set; }

        public int CitiesNumber { get; set; }
        
        public int ChannelsNumber { get; set; }

        public int ProvidersNumber { get; set; }
        
        public string Path { get; set; }
    }
}