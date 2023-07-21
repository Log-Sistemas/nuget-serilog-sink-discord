using LogSistemas.Nuget.Serilog.Sinks.Discord;

using Serilog;
using Serilog.Context;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .WriteTo.Discord(
        webhookUrl: "",
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        new Property("location", "User city"),
        new Property("correlationid", "Correlation-id"),
        new Property("user-id", "Invalid key"))
    .CreateLogger();

//Add yours custom properties
LogContext.PushProperty("correlationid", Guid.NewGuid());
LogContext.PushProperty("location", "Rio de Janeiro");

Log.Information("This a information message");
try
{
    throw new FileNotFoundException();
}
catch (Exception ex)
{
    Log.Error(ex, "Oww crap");
}

Log.CloseAndFlush();