# Discord sink

[![Nuget](https://img.shields.io/nuget/v/LogSistemas.Nuget.Serilog.Sinks.Discord?style=for-the-badge&logo=nuget&labelColor=%23004880&color=%23004880)](https://www.nuget.org/packages/LogSistemas.Nuget.Discord.Messenger/) 
![Nuget](https://img.shields.io/nuget/dt/LogSistemas.Nuget.Serilog.Sinks.Discord?style=for-the-badge&logo=nuget&labelColor=%23004880&color=%23004880)

This is a .Net serilog sink to send message to discord webhook.
Has configuration to choose some user claims to send in the message.

I was build with dotnet 6.0.

## How to use

You can customize 

### Simple use

```C#
.WriteTo.Discord(
	webhookUrl: discordWebhookUrl,
	restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
```

### Custom properties

```C#
.WriteTo.Discord(
	webhookUrl: discordWebhookUrl,
	claims: new List<ClaimConfig>{
		new ClaimConfig{
			Name = "RequestId",
			DisplayTitle = "ID from request"
		},
		new ClaimConfig{
			Name = "RequestPath",
			DisplayTitle = "PATH from request"
		},
	}
	restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
```