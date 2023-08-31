#nullable enable
using System;

namespace Saladim.SalLogger;

public partial class LoggerBuilder
{
    private LogAction? actions;
    private LogFormatter? formatter;
    private LogLevel levelLimit = LogLevel.Info;
    private bool useCustomFormatter = false;

    public Logger Build()
    {
        Logger logger = new()
        {
            LogAction = actions,
            LogLevelLimit = levelLimit
        };
        if (useCustomFormatter)
        {
            logger.Formatter = formatter;
        }
        return logger;
    }

    public LoggerBuilder WithLevelLimit(LogLevel lowestLogLevel)
    {
        levelLimit = lowestLogLevel;
        return this;
    }

    public LoggerBuilder WithAction(LogAction action)
    {
        actions += action;
        return this;
    }

    public LoggerBuilder WithFormatter(LogFormatter formatter)
    {
        this.useCustomFormatter = true;
        this.formatter = formatter;
        return this;
    }

    public LoggerBuilder WithLogToConsole()
        => WithAction(Console.WriteLine);
}