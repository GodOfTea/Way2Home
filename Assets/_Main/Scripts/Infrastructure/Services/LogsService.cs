namespace Infrastructure.Services
{
    public class LogsService : IService
    {
        private const string LOGS_PATH = "Logs/";

        public void AddLog(string message, LogType logType)
        {
            
        }
        
        public void ClearLogs()
        {
            
        }
    }
    
    public enum LogType
    {
        Game,
        Event
    }
}