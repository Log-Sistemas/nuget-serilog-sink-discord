using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace LogSistemas.Nuget.Serilog.Sinks.Discord
{
    public static class Extension
    {
        /// <summary>
        /// Add discord sink integration
        /// </summary>
        /// <param name="loggerConfiguration"></param>
        /// <param name="webhookUrl">Notification destiny</param>
        /// <param name="sendExceptionDetails">If false, Stack trace and Exception type will not be sent in notification</param>
        /// <param name="restrictedToMinimumLevel"></param>
        /// <param name="propertiesFromLog">List of log event properties that will be added in the notification. Properties must be added previously with LogContext.PushProperty</param>
        /// <returns></returns>
        public static LoggerConfiguration Discord(
            this LoggerSinkConfiguration loggerConfiguration,
            string webhookUrl,
            bool sendExceptionDetails = false,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose,
            params Property[] propertiesFromLog)
        {
            return loggerConfiguration.Sink(new Sink(webhookUrl, sendExceptionDetails, restrictedToMinimumLevel, propertiesFromLog));
        }
    }
}