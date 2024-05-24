using System;
using System.Text;

namespace Saladim.SalLogger;

public delegate void LogHandler(LogLevel logLevel, string section, string? subsection, string content);
public delegate string LogFormatter(LogLevel logLevel, string section, string? subsection, string content);
public delegate void FormattedLogHandler(string log);

public sealed partial class Logger
{
    private LogHandler? handler;

    public LogLevel LogLevelLimit { get; set; }

    public Logger()
    {
        LogLevelLimit = LogLevel.Info;
    }

    public Logger(LogLevel logLevelLimit)
    {
        if ((int)logLevelLimit is < 0 or > ((int)LogLevel.Fatal))
            throw new ArgumentOutOfRangeException(nameof(logLevelLimit));
        LogLevelLimit = logLevelLimit;
    }

    public static string DefaultFormatLog(LogLevel logLevel, string section, string? subsection, string content)
        => $"[{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f}] [{logLevel}:{section}" + (subsection is null ? "" : $"/{subsection}") + $"]: {content}";

    public bool ShouldLog(LogLevel logLevel)
        => (int)logLevel >= (int)LogLevelLimit;

    public void Log(LogLevel logLevel, string section, string? subsection, string content)
    {
        if (!ShouldLog(logLevel)) return;
        handler?.Invoke(logLevel, section, subsection, content);
    }

    public void AddLogHandler(LogHandler handler)
    {
        if (handler is null)
            throw new ArgumentNullException(nameof(handler));
        this.handler += handler;
    }

    public void AddLogHandler(FormattedLogHandler handler)
    {
        if (handler is null)
            throw new ArgumentNullException(nameof(handler));
        AddLogHandler(handler, DefaultFormatLog);
    }

    public void AddLogHandler(FormattedLogHandler handler, LogFormatter formatter)
    {
        if (handler is null)
            throw new ArgumentNullException(nameof(handler));
        if (formatter is null)
            throw new ArgumentNullException(nameof(formatter));
        AddLogHandler((logLevel, section, subsection, content) => handler(formatter(logLevel, section, subsection, content)));
    }

    public void Log(LogLevel logLevel, string section, string content)
        => Log(logLevel, section, subsection: null, content);

    public void Log(LogLevel logLevel, string section, string? subsection, Exception exception, string? prefix = null)
    {
        if (!ShouldLog(logLevel)) return;
        bool writePrefix = prefix is not null;
        if (writePrefix)
        {
            StringBuilder sb = new();
            {
                sb.AppendLine(prefix);
                sb.Append("   ");
                sb.Append(exception.ToString());
            }
            Log(logLevel, section, subsection, sb.ToString());
        }
        else
        {
            Log(logLevel, section, subsection, exception.ToString());
        }
    }

    public void Log(LogLevel logLevel, string section, Exception exception, string? prefix = null)
        => Log(logLevel, section, null, exception, prefix);
}

// TODO xml document comment