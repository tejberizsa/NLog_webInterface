using System;

namespace LogiAppMonitor.API.Models
{
    public class LogMessage
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Instance { get; set; }
        public string LogLevel { get; set; }
        public string Logger { get; set; }
        public string LoggerMessage { get; set; }
    }
}