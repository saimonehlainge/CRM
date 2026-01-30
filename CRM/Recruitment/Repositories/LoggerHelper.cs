namespace Recruitment.Repositories
{
    public interface ILoggerHelperRepository
    {
        void LogMessage(string message);
        void LogMessage(Exception ex);
    }

    public class LoggerHelper : ILoggerHelperRepository
    {
        private readonly ILogger<ILoggerFactory> _logger;

        public LoggerHelper(ILogger<ILoggerFactory> logger)
        {
            _logger = logger;
        }
        public void LogMessage(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogMessage(Exception ex)
        {
            _logger.LogError($"Error:{ex.Message},\n {ex.StackTrace}");
        }
    }
}
