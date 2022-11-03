#nullable enable

using System;
using System.Collections.Generic;
using System.Text;

namespace Saladim.SalLogger;

public delegate string LogFormatter(
    LogLevel logLevel,
    string section,
    string? subSection,
    string content
    );

public delegate void LogAction(string content);

public class Logger
{
    internal static readonly LogFormatter DefaultFormatter = DefaultFormatAction;
    internal static string DefaultFormatAction(LogLevel logLevel, string section, string? subSection, string content)
        => $"[{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f}] [{logLevel}:{section}" + (subSection is null ? "" : $"/{subSection}") + $"]: {content}";

    internal LogFormatter? Formatter = DefaultFormatter;
    internal LogAction? LogAction;
    internal LogLevel LogLevelLimit = LogLevel.Info;

    internal Logger() { }

    protected void LogRaw(string content)
    {
        LogAction?.Invoke(content);
    }

    protected bool NeedLogging(LogLevel logLevel)
        => (int)logLevel >= (int)LogLevelLimit;

    #region 各种各样的log

    #region 一些带LogLevel参数的log

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
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
    {
        if (!NeedLogging(logLevel)) return;
        StringBuilder sb = new();
        bool writePrefix = prefix is not null;
        if (extractChain)
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
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(logLevel, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #region Trace

    public void LogTrace(string section, string? subSection, string content)
        => Log(LogLevel.Trace, section, subSection, content);

    public void LogTrace(string section, string content)
        => Log(LogLevel.Trace, section, null, content);

    public void LogTrace(string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Trace, section, subSection, exception, prefix, suffix, extractChain);

    public void LogTrace(string section,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Trace, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #region Debug

    public void LogDebug(string section, string? subSection, string content)
        => Log(LogLevel.Debug, section, subSection, content);

    public void LogDebug(string section, string content)
        => Log(LogLevel.Debug, section, null, content);

    public void LogDebug(string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Debug, section, subSection, exception, prefix, suffix, extractChain);

    public void LogDebug(string section,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Debug, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #region Info

    public void LogInfo(string section, string? subSection, string content)
        => Log(LogLevel.Info, section, subSection, content);

    public void LogInfo(string section, string content)
        => Log(LogLevel.Info, section, null, content);

    public void LogInfo(string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Info, section, subSection, exception, prefix, suffix, extractChain);

    public void LogInfo(string section,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Info, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #region Warn

    public void LogWarn(string section, string? subSection, string content)
        => Log(LogLevel.Warn, section, subSection, content);

    public void LogWarn(string section, string content)
        => Log(LogLevel.Warn, section, null, content);

    public void LogWarn(string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Warn, section, subSection, exception, prefix, suffix, extractChain);

    public void LogWarn(string section,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Warn, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #region Error

    public void LogError(string section, string? subSection, string content)
        => Log(LogLevel.Error, section, subSection, content);

    public void LogError(string section, string content)
        => Log(LogLevel.Error, section, null, content);

    public void LogError(string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Error, section, subSection, exception, prefix, suffix, extractChain);

    public void LogError(string section,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Error, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #region Fatal

    public void LogFatal(string section, string? subSection, string content)
        => Log(LogLevel.Fatal, section, subSection, content);

    public void LogFatal(string section, string content)
        => Log(LogLevel.Fatal, section, null, content);

    public void LogFatal(string section, string? subSection,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Fatal, section, subSection, exception, prefix, suffix, extractChain);

    public void LogFatal(string section,
        Exception exception, string? prefix = null, string? suffix = null, bool extractChain = false)
        => Log(LogLevel.Fatal, section, null, exception, prefix, suffix, extractChain);

    #endregion

    #endregion
}
