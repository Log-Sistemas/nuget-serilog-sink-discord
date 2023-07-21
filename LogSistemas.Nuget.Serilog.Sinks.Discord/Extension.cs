using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace LogSistemas.Nuget.Serilog.Sinks.Discord
{
    public static class Extension
    {
        public static LoggerConfiguration Discord(
                this LoggerSinkConfiguration loggerConfiguration,
                string webhookUrl,
                LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose,
                params Property[] propertiesFromLog)
        {
            return loggerConfiguration.Sink(new Sink(webhookUrl, restrictedToMinimumLevel, propertiesFromLog));
        }
    }
}