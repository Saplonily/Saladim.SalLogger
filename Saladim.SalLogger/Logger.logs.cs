using System;

namespace Saladim.SalLogger;

public partial class Logger
{
    #region Trace

    public void LogTrace(string section, string subSection, string content)
        => Log(LogLevel.Trace, section, subSection, content);

    public void LogTrace(string section, string content)
        => Log(LogLevel.Trace, section, null, content);

    public void LogTrace(string section, string subSection, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Trace, section, subSection, exception, prefix, suffix, autoExtractChain);

    public void LogTrace(string section, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Trace, section, null, exception, prefix, suffix, autoExtractChain);

    #endregion

    #region Debug

    public void LogDebug(string section, string subSection, string content)
        => Log(LogLevel.Debug, section, subSection, content);

    public void LogDebug(string section, string content)
        => Log(LogLevel.Debug, section, null, content);

    public void LogDebug(string section, string subSection, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Debug, section, subSection, exception, prefix, suffix, autoExtractChain);

    public void LogDebug(string section, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Debug, section, null, exception, prefix, suffix, autoExtractChain);

    #endregion

    #region Info

    public void LogInfo(string section, string subSection, string content)
        => Log(LogLevel.Info, section, subSection, content);
    public void LogInfo(string section, string content)
        => Log(LogLevel.Info, section, null, content);

    public void LogInfo(string section, string subSection, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Info, section, subSection, exception, prefix, suffix, autoExtractChain);

    public void LogInfo(string section, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Info, section, null, exception, prefix, suffix, autoExtractChain);

    #endregion

    #region Warn

    public void LogWarn(string section, string subSection, string content)
        => Log(LogLevel.Warn, section, subSection, content);


    public void LogWarn(string section, string content)
        => Log(LogLevel.Warn, section, null, content);

    public void LogWarn(string section, string subSection, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Warn, section, subSection, exception, prefix, suffix, autoExtractChain);

    public void LogWarn(string section, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Warn, section, null, exception, prefix, suffix, autoExtractChain);

    #endregion

    #region Error

    public void LogError(string section, string subSection, string content)
        => Log(LogLevel.Error, section, subSection, content);

    public void LogError(string section, string content)
        => Log(LogLevel.Error, section, null, content);

    public void LogError(string section, string subSection, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Error, section, subSection, exception, prefix, suffix, autoExtractChain);

    public void LogError(string section, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Error, section, null, exception, prefix, suffix, autoExtractChain);

    #endregion

    #region Fatal

    public void LogFatal(string section, string subSection, string content)
        => Log(LogLevel.Fatal, section, subSection, content);

    public void LogFatal(string section, string content)
        => Log(LogLevel.Fatal, section, null, content);

    public void LogFatal(string section, string subSection, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Fatal, section, subSection, exception, prefix, suffix, autoExtractChain);

    public void LogFatal(string section, Exception exception,
        string prefix = null, string suffix = null, bool autoExtractChain = true)
        => Log(LogLevel.Fatal, section, null, exception, prefix, suffix, autoExtractChain);

    #endregion
}