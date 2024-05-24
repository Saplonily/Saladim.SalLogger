using System;
using System.IO;
using Saladim.SalLogger;

namespace Sample;

public class Program
{
    public static void Main()
    {
        {
            Logger logger = new Logger(LogLevel.Info);
            logger.AddLogHandler(Console.WriteLine);

            logger.Log(LogLevel.Info, "SectionName", "Your log message.");
            logger.Log(LogLevel.Warn, "SectionName", "SubsectionName", "Your log message.");
            logger.LogInfo("SectionName", "Easier way to log info message.");
            logger.LogFatal("SectionName", "Subsection", "Easier way to log fatal message with subsection.");
            logger.LogTrace("SectionName", "This log message will not be logged because we limit the `LevelLimit` to `Info`");
        }

        {
            static string MyFormatter(LogLevel logLevel, string section, string subsection, string content)
                => $"「{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f} {logLevel} {section}" +
                $"{(subsection is null ? "" : $"/{subsection}")}」 {content}";

            using StreamWriter writer = new("log.txt");
            Logger logger = new Logger(LogLevel.Debug);
            logger.AddLogHandler(Console.WriteLine, MyFormatter);
            logger.AddLogHandler(writer.WriteLine, MyFormatter);

            logger.LogInfo("SomeSection", "A  log.");
            logger.LogFatal("SomeSection", "Another fatal log");
            logger.LogTrace("AnotherSection", "A trace log.");
        }
        {
            Logger logger = new(LogLevel.Trace);
            logger.AddLogHandler(Console.WriteLine);

            logger.LogDebug("Main", "some content...");
            try
            {
                throw new Exception("Something went wrong.");
            }
            catch (Exception ex)
            {
                logger.LogInfo("Main", ex, prefix: "prefix");
                try
                {
                    throw new Exception("Something else went wrong because `Something went wrong.`", ex);
                }
                catch (Exception ex2)
                {
                    logger.LogError("Main", ex2);
                    logger.LogError("Main", ex2);
                    logger.LogError("Main", ex2, prefix: "prefix111");
                }
            }
        }
    }
}
