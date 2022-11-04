using System;
using System.IO;
using Saladim.SalLogger;

namespace Sample;

public class Program
{
    static void Main(string[] args)
    {
        /**
        LoggerBuilder builder = new();
        builder.WithLevelLimit(LogLevel.Info)
               .WithLogToConsole()
               .WithFormatter()

        Logger logger = builder.Build();

        logger.Log(LogLevel.Info, "sectionName", "Your log message.");
        logger.Log(LogLevel.Warn, "sectionName", "subSectionName", "Your log message.");
        logger.LogInfo("sectionName", "easier to log info message.");
        logger.LogFatal("sectionName", "subSection", "you can also have `subSection` in easier way.");
        logger.LogTrace("sectionName", "this log message will not be logged because we limit the `LevelLimit` to `Info`");
        */
        
        {
            using StreamWriter writer = new("log.txt");

            LoggerBuilder builder = new();
            builder.WithLevelLimit(LogLevel.Debug)
                   .WithLogToConsole()
                   .WithAction(s => writer.WriteLine(s))
                   .WithFormatter(MyFormatter);
            static string MyFormatter(LogLevel logLevel, string section, string subSection, string content)
                => $"「{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f} {logLevel} {section}" +
                $"{(subSection is null ? "" : $"/{subSection}")}」 {content}";

            Logger logger = builder.Build();

            logger.LogInfo("SomeSection", "这是一条log");
            logger.LogFatal("SomeSection", "另一条fatal log");
            logger.LogTrace("AnotherSection", "不会显示的log");
        }
        {
            LoggerBuilder builder = new();
            builder.WithLevelLimit(LogLevel.Trace)
                   .WithLogToConsole();
            Logger logger = builder.Build();
            logger.LogDebug("Main", "一些内容");
            try
            {
                throw new Exception("Something went fucky wrong.");
            }
            catch (Exception ex)
            {
                logger.LogInfo("Main", ex, prefix: "这是前缀", suffix: "这是后缀");
                try
                {
                    throw new Exception("Something else went fucky wrong because `Something went fucky wrong.`", ex);
                }
                catch (Exception ex2)
                {
                    logger.LogError("Main", ex2, suffix: "这是后缀");
                    logger.LogError("Main", ex2);
                    logger.LogError("Main", ex2, prefix: "前缀11111", suffix: "后缀11111");
                }
            }
        }
    }
}
