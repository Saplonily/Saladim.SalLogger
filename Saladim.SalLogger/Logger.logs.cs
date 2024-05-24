using System;

namespace Saladim.SalLogger;

public partial class Logger
{
    #region Trace

    public void LogTrace(string section, string subsection, string content)
        => Log(LogLevel.Trace, section, subsection, content);

    public void LogTrace(string section, string content)
        => Log(LogLevel.Trace, section, subsection: null, content);

    public void LogTrace(string section, string subsection, Exception exception, string? prefix = null)
        => Log(LogLevel.Trace, section, subsection, exception, prefix);

    public void LogTrace(string section, Exception exception, string? prefix = null)
        => Log(LogLevel.Trace, section, null, exception, prefix);

    #endregion

    #region Debug

    public void LogDebug(string section, string subsection, string content)
        => Log(LogLevel.Debug, section, subsection, content);

    public void LogDebug(string section, string content)
        => Log(LogLevel.Debug, section, subsection: null, content);

    public void LogDebug(string section, string subsection, Exception exception, string? prefix = null)
        => Log(LogLevel.Debug, section, subsection, exception, prefix);

    public void LogDebug(string section, Exception exception, string? prefix = null)
        => Log(LogLevel.Debug, section, null, exception, prefix);

    #endregion

    #region Info

    public void LogInfo(string section, string subsection, string content)
        => Log(LogLevel.Info, section, subsection, content);

    public void LogInfo(string section, string content)
        => Log(LogLevel.Info, section, subsection: null, content);

    public void LogInfo(string section, string subsection, Exception exception, string? prefix = null)
        => Log(LogLevel.Info, section, subsection, exception, prefix);

    public void LogInfo(string section, Exception exception, string? prefix = null)
        => Log(LogLevel.Info, section, null, exception, prefix);

    #endregion

    #region Warn

    public void LogWarn(string section, string subsection, string content)
        => Log(LogLevel.Warn, section, subsection, content);

    public void LogWarn(string section, string content)
        => Log(LogLevel.Warn, section, subsection: null, content);

    public void LogWarn(string section, string subsection, Exception exception, string? prefix = null)
        => Log(LogLevel.Warn, section, subsection, exception, prefix);

    public void LogWarn(string section, Exception exception, string? prefix = null)
        => Log(LogLevel.Warn, section, null, exception, prefix);

    #endregion

    #region Error

    public void LogError(string section, string subsection, string content)
        => Log(LogLevel.Error, section, subsection, content);

    public void LogError(string section, string content)
        => Log(LogLevel.Error, section, subsection: null, content);

    public void LogError(string section, string subsection, Exception exception, string? prefix = null)
        => Log(LogLevel.Error, section, subsection, exception, prefix);

    public void LogError(string section, Exception exception, string? prefix = null)
        => Log(LogLevel.Error, section, null, exception, prefix);

    #endregion

    #region Fatal

    public void LogFatal(string section, string subsection, string content)
        => Log(LogLevel.Fatal, section, subsection, content);

    public void LogFatal(string section, string content)
        => Log(LogLevel.Fatal, section, subsection: null, content);

    public void LogFatal(string section, string subsection, Exception exception, string? prefix = null)
        => Log(LogLevel.Fatal, section, subsection, exception, prefix);

    public void LogFatal(string section, Exception exception, string? prefix = null)
        => Log(LogLevel.Fatal, section, null, exception, prefix);

    #endregion
}