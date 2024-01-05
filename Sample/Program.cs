using LogSistemas.Nuget.Serilog.Sinks.Discord;

using Serilog;
using Serilog.Context;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.FromLogContext()
    .WriteTo.Discord(
        webhookUrl: "https://discord.com/api/webhooks/1129054786839400519/pca8UCwW8X5l8UpwQ2GlZ04XAhVNe9HlFn0Y890IQYixbQ9FQ3H0eCbzfcWn0_emyWjH",
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

Log.Information("This is a ruge text with 4097, one more than the limit 4096. At amet ipsum et nonumy magna facilisi stet odio nonumy stet sed duo eirmod nonumy consectetuer aliquyam sed dolor et velit dolore sadipscing gubergren diam sed dolores erat accusam accumsan no feugait eos assum lorem duo justo erat soluta clita diam accumsan dolor sed sit elitr tempor eos sit erat at nam ut et odio no in tempor diam dolor ea consetetur eros clita elit aliquip duo aliquyam dolor et praesent dolor nulla nonumy autem kasd duis dolor stet labore est nibh ut volutpat et stet vero et et amet sea at ipsum est ipsum labore dolor nonumy eu kasd invidunt sanctus lorem at sit invidunt rebum sit magna no gubergren takimata rebum elitr sadipscing lorem gubergren sanctus duis sadipscing feugiat minim ipsum takimata consetetur takimata lorem stet nostrud autem stet consetetur et dolore euismod ipsum sed feugait illum consetetur takimata diam eum eos ut ipsum aliquyam kasd stet consetetur vero elitr voluptua tempor facilisis in no ea aliquyam amet sed dolore est voluptua amet dolor sit sadipscing ipsum consetetur ad justo et blandit ut lorem labore at nonumy diam sit eirmod sea zzril tincidunt et dolores volutpat takimata invidunt justo id facilisis accumsan ipsum sit est odio diam wisi diam velit stet justo amet magna justo sadipscing ut sadipscing et vel sadipscing nonumy dolore ipsum ex vulputate aliquam vero duo stet invidunt clita vulputate duo nonumy zzril feugiat nonumy option stet dolor assum erat no at tation et amet dolor sed ea vel amet lorem sit velit takimata consectetuer diam et invidunt lorem eleifend vero sit amet sed illum amet labore autem sit dolor imperdiet iusto iusto diam vero diam sit feugait ut est diam duis aliquam sit ipsum ipsum labore accumsan aliquyam clita consequat diam diam eirmod dolore vero at nibh blandit sit autem facilisis eos sit duis rebum sed diam labore exerci dolore euismod dolor volutpat magna kasd nostrud amet eros amet consequat congue et dolore et blandit aliquyam illum est magna dolor esse sanctus wisi vulputate ipsum ipsum labore ipsum nonumy et magna laoreet facer feugiat sea gubergren no eirmod et rebum at duo et justo sit eos takimata elitr ea qui sed dolore dolor sanctus delenit wisi dolores diam euismod takimata euismod ipsum iusto eos illum eos at duo accusam no et dolores qui sit sea ut labore eirmod possim amet rebum te tempor ipsum sed enim eirmod labore sit takimata quod dolore sea aliquyam est at ea illum illum sed sanctus sed et takimata dolores voluptua ad dignissim sit lorem illum soluta sanctus dolore eros duo consetetur justo exerci dolor ut ut praesent stet dolore eirmod nonummy ut sit amet sit duo sed clita clita vero possim aliquyam tempor ea lorem tempor eirmod dolor suscipit diam iriure duo in ipsum dolor stet et amet consetetur nulla magna sea sed erat duo diam et adipiscing ipsum dolores justo sadipscing sit amet nihil enim dolore gubergren dolor magna assum duis eos hendrerit hendrerit no invidunt sadipscing magna tempor autem amet labore sed duo nulla sea sea et rebum autem accumsan lorem dolor et commodo sanctus sit accumsan eirmod dolore stet consequat sit voluptua at lobortis autem tempor no lorem iriure amet gubergren lorem dignissim gubergren congue dolor et sanctus vero et dolore duo rebum autem magna dolore voluptua cum et eirmod sadipscing sanctus consectetuer consectetuer et tation consetetur voluptua ullamcorper accusam no sadipscing duo vero consetetur sit invidunt takimata tempor dolores lobortis dolore sea eirmod duo amet sit nihil ut sit kasd at aliquip nibh vero est sea et tempor elitr no diam ea ipsum ut exerci duo vulputate dolor aliquyam invidunt ipsum sed sit zzril amet ipsum consetetur veniam vero amet eum odio ipsum gubergren labore et gubergren sit diam at aliquyam justo invidunt no dolore ut lorem tempor sed amet ullamcorper et consetetur voluptua elitr ea justo kasd clita sanctus dolor nibh kasd aliquyam sit eirmod duo diam amet ut eos velit erat dignissim sanctus eirmod at gubergren autem gubergren amet tation clita dolore velit esse sea labore dolores et justoxx");

Log.CloseAndFlush();
