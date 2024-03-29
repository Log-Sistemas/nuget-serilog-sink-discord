﻿using System;
using System.Collections.Generic;

using Discord;
using Discord.Webhook;

using Serilog.Core;
using Serilog.Events;

namespace LogSistemas.Nuget.Serilog.Sinks.Discord
{
    public class Sink : ILogEventSink
    {
        private readonly string _webhookUrl;
        private readonly bool _sendExceptionDetails;
        private readonly IEnumerable<Property> _propertiesFromLog;
        private readonly LogEventLevel _restrictedToMinimumLevel;

        public Sink(
            string webhookUrl,
            bool sendExceptionDetails = true,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Information,
            params Property[] propertiesFromLog)
        {
            _webhookUrl = webhookUrl;
            _sendExceptionDetails = sendExceptionDetails;
            _propertiesFromLog = propertiesFromLog;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            SendMessage(logEvent);
        }

        private void SendMessage(LogEvent logEvent)
        {
            if (!ShouldLogMessage(_restrictedToMinimumLevel, logEvent.Level))
                return;

            if (string.IsNullOrEmpty(_webhookUrl))
                return;

            EmbedBuilder embedBuilder = new();
            using DiscordWebhookClient client = new(_webhookUrl);
            try
            {
                if (logEvent.Exception != null)
                {
                    embedBuilder
                        .WithColor(255, 0, 0)
                        .WithTitle(":x: Exception")
                        .AddField("Exception message:", FormatMessage(logEvent.Exception.Message, 1000))
                        .AddField("Log message:", FormatMessage(logEvent.RenderMessage(), 1000));
                    if (_sendExceptionDetails)
                    {
                        embedBuilder
                            .WithDescription(FormatMessage($"StackTrace: {logEvent.Exception.StackTrace!}", 4096))
                            .AddField("Type:", $"```{logEvent.Exception.GetType().FullName}```");
                    }
                }
                else
                {
                    SpecifyTitleLevel(logEvent.Level, embedBuilder);
                    embedBuilder
                        .WithDescription(FormatMessage(logEvent.RenderMessage(), 4096));
                }

                if (_propertiesFromLog is not null)
                {
                    foreach (Property item in _propertiesFromLog)
                    {
                        embedBuilder.AddField(
                            name: item.Display,
                            value: FormatMessage(logEvent.GetPropValueOrDefault(item.Name), 1000));
                    }
                }

                client.SendMessageAsync(null, false, new Embed[] { embedBuilder.Build() })
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception ex)
            {
                client.SendMessageAsync(
                    $"ooo snap, {ex.Message}", false)
                    .GetAwaiter()
                    .GetResult();
                throw;
            }
        }

        private static void SpecifyTitleLevel(LogEventLevel level, EmbedBuilder embedBuilder)
        {
            switch (level)
            {
                case LogEventLevel.Verbose:
                    embedBuilder.Title = ":loud_sound: Verbose";
                    embedBuilder.Color = Color.LightGrey;
                    break;

                case LogEventLevel.Debug:
                    embedBuilder.Title = ":mag: Debug";
                    embedBuilder.Color = Color.LightGrey;
                    break;

                case LogEventLevel.Information:
                    embedBuilder.Title = ":information_source: Information";
                    embedBuilder.Color = new Color(0, 186, 255);
                    break;

                case LogEventLevel.Warning:
                    embedBuilder.Title = ":warning: Warning";
                    embedBuilder.Color = new Color(255, 204, 0);
                    break;

                case LogEventLevel.Error:
                    embedBuilder.Title = ":x: Error";
                    embedBuilder.Color = new Color(255, 0, 0);
                    break;

                case LogEventLevel.Fatal:
                    embedBuilder.Title = ":skull_crossbones: Fatal";
                    embedBuilder.Color = Color.DarkRed;
                    break;

                default:
                    break;
            }
        }

        public static string FormatMessage(string message, int maxLenght)
        {
            const int LENGHT_TRIPLE_QUOTES = 6;
            const int LENGHT_ELLIPSIS = 4;
            int finalMaxLenght = maxLenght - LENGHT_TRIPLE_QUOTES - LENGHT_ELLIPSIS;

            if (string.IsNullOrWhiteSpace(message))
                return message;

            string quotedMessage = $"```{message}```";

            if (quotedMessage.Length > maxLenght)
                quotedMessage = $"```{message.Substring(0, finalMaxLenght)} ...```";

            return quotedMessage;
        }

        private static bool ShouldLogMessage(
            LogEventLevel minimumLogEventLevel,
            LogEventLevel messageLogEventLevel) =>
                (int)messageLogEventLevel < (int)minimumLogEventLevel ? false : true;
    }

    internal static class LogEventExtensions
    {
        public static string GetPropValueOrDefault(this LogEvent logEvent, string propertyName)
        {
            string value = $"key {propertyName} was not present in log event";
            if (logEvent.Properties.ContainsKey(propertyName))
                value = logEvent.Properties[propertyName].ToString();

            return value;
        }
    }

    public class Property
    {
        public Property(string name, string display)
        {
            Name = name;
            Display = display;
        }

        public string Name { get; }
        public string Display { get; }
    }
}