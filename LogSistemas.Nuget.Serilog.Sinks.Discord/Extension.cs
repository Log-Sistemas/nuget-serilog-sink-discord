using System.Collections.Generic;

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
                IEnumerable<ClaimConfig> claims = null,
                LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose)
        {
            return loggerConfiguration.Sink(new Sink(webhookUrl, claims, restrictedToMinimumLevel));
        }
    }
}