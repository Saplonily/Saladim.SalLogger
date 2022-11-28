#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Saladim.SalLogger;

public delegate string LogFormatter(
    LogLevel logLevel,
    string section,
    string? subSection,
    string content
    );

public delegate void LogAction(string content);

public partial class Logger
{
    internal static readonly LogFormatter DefaultFormatter = DefaultFormatAction;
    internal static string DefaultFormatAction(LogLevel logLevel, string section, string? subSection, string content)
        => $"[{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f}] [{logLevel}:{section}" + (subSection is null ? "" : $"/{subSection}") + $"]: {content}";

    internal LogFormatter? Formatter = DefaultFormatter;
    internal LogAction? LogAction;
    internal LogLevel LogLevelLimit = LogLevel.Info;

    internal Logger() { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected void LogRaw(string content)
    {
        LogAction?.Invoke(content);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NeedLogging(LogLevel logLevel)
        => (int)logLevel >= (int)LogLevelLimit;

    public void LogRaw(LogLevel logLevel, string str)
    {
        if (NeedLogging(logLevel))
        {
            LogRaw(str);
        }
    }

    public void Log(LogLevel logLevel, string section, string? subSection, string content)
    {
        if (!NeedLogging(logLevel)) return;
        string? str = Formatter?.Invoke(logLevel, section, subSection, content);
        if (str is null) return;
        LogRaw(str);
    }

    public void Log(LogLevel logLevel, string section, string content)
        => Log(logLevel, section, null, content);

    public void Log(LogLevel logLevel, string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool autoExtractChain = true)
    {
        if (!NeedLogging(logLevel)) return;
        StringBuilder sb = new();
        bool writePrefix = prefix is not null;
        if (exception.InnerException is not null && autoExtractChain)
        {
            var exs = ExceptionHelper.GetChainedExceptions(exception);
            var firstException = exs[0];
            if (writePrefix)
            {
                sb.AppendLine(prefix);
                sb.Append("   - ");
            }
            sb.AppendLine($"{firstException.GetType()} - {firstException.Message}");
            var enumator = exs.GetEnumerator();
            enumator.MoveNext();
            while (enumator.MoveNext())
            {
                sb.Append("   - ");
                sb.AppendLine($"{enumator.Current.GetType()} - {enumator.Current.Message}");
                sb.AppendLine(enumator.Current.StackTrace);
            }
        }
        else
        {
            if (writePrefix)
            {
                sb.AppendLine(prefix);
                sb.Append("   ");
            }
            sb.AppendLine($"{exception.GetType()}  -  {exception.Message}");
            sb.Append(exception.StackTrace);
            if (writePrefix) sb.AppendLine();
        }
        if (suffix is not null) sb.Append("   " + suffix);
        this.Log(logLevel, section, subSection, sb.ToString());
    }

    public void Log(LogLevel logLevel, string section,
        Exception exception, string? prefix = null, string? suffix = null, bool autoExtractChain = true)
        => Log(logLevel, section, null, exception, prefix, suffix, autoExtractChain);

}
