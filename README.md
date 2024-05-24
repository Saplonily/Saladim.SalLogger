# Saladim.SalLogger

<div align="middle">
    一个被建议用于游戏/小框架的轻量级日志框架 (或者叫做代码片段更合适)<br>
    A lightweight logging framework (or better called a code snippet) recommended for games/small frameworks.
</div>

## 引用 Reference
你可以到 NuGet 上搜索 [`SalLogger`](https://www.nuget.org/packages/Saladim.SalLogger/) 以 NuGet 包形式安装,
或者直接引入项目全代码(在 [gist](https://gist.github.com/Saplonily/2b23e2cfa00223fa27c483af098d27e1) 中)  
You can search for [`SalLogger`](https://www.nuget.org/packages/Saladim.SalLogger/) on NuGet to install it as a NuGet package,
or directly include the entire project code (available on [gist](https://gist.github.com/Saplonily/2b23e2cfa00223fa27c483af098d27e1)).

## 构建 Build
项目本体很简单, 只需要简单的 `dotnet build` / 点 IDE 的一下 `生成` 键.  
The project itself is very simple, just use dotnet build or click the Build button in your IDE.

## 使用 Usage
核心类为 `Logger`, 位于 `Saladim.SalLogger` 命名空间下,
可以使用构造器直接构造一个 `Logger`:  
The core class is `Logger`, located in the `Saladim.SalLogger` namespace.
You can directly instantiate a `Logger` using its constructor:

```C#
Logger logger = new Logger(LogLevel.Info);
logger.AddLogHandler(Console.WriteLine);
```

构造器的 `LogLevel` 类型的参数限制了 `Logger` 输出的最低等级, 日志等级 `LogLevel` 分为:  
The constructor's `LogLevel` parameter sets the minimum logging level for the `Logger`. The `LogLevel` enum has the following levels:  

- `Trace`
- `Debug`
- `Info`
- `Warn`
- `Error`
- `Fatal`

如果将限制等级设置为 `Info`, 那么 `Debug` 与 `Trace` 将不会被记录, 其余同理.  
If you set the logging level to `Info`, `Debug` and `Trace` messages will not be recorded. The same applies to other levels.  

`Logger` 对象有许多重载方法, 可以选择适合情景的重载版本:  
The `Logger` class provides various overloaded methods to suit different scenarios:  

```C#
logger.Log(LogLevel.Info, "SectionName", "Your log message.");
logger.Log(LogLevel.Warn, "SectionName", "SubsectionName", "Your log message.");
logger.LogInfo("SectionName", "Easier way to log info message.");
logger.LogFatal("SectionName", "Subsection", "Easier way to log fatal message with subsection.");
logger.LogTrace("SectionName", "This log message will not be logged because we limit the `LevelLimit` to `Info`");
```

将会产生如下输出(控制台):  
This will produce the following output (in the console):  

```log
[20:47:45.9] [Info:SectionName]: Your log message.
[20:47:45.9] [Warn:SectionName/SubsectionName]: Your log message.
[20:47:45.9] [Info:SectionName]: Easier way to log info message.
[20:47:45.9] [Fatal:SectionName/Subsection]: Easier way to log fatal message with subsection.
```

## 更多实例 More Examples
如果你不满足于目前的日志格式, 那么你可以使用 `AddLogHandler` 的 `LogHandler` 重载, 它会将传递所有的原始日志参数,
例如你可以获取其中的 `logLevel` 参数并对日志上色.  
If you are not satisfied with the current log format, you can use the `LogHandler` overload of `AddLogHandler`.
This method passes all the original log parameters,
allowing you to customize the log format, such as coloring the logs based on the `logLevel`.  

`LogHandler` 原型如下:  
The `LogHandler` delegate is defined as follows:
```C#
public delegate void LogHandler(LogLevel logLevel, string section, string? subsection, string content);
```

当未指定 `subsection` 时 `null` 会被传入.  
When the `subsection` is not specified, `null` will be passed.  

此外还可以只单独指定格式化器, 例如使用如下格式化器实例:  
You can also specify only a formatter, like the following example:  

```C#
static string MyFormatter(LogLevel logLevel, string section, string subsection, string content)
    => $"「{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f} {logLevel} {section}" +
    $"{(subsection is null ? "" : $"/{subsection}")}」 {content}";
```

并使用 `AddLogHandler` 的 `(FormattedLogHandler, LogFormatter)` 重载, 可以得到以下日志:  
Using the `(FormattedLogHandler, LogFormatter)` overload of `AddLogHandler`, you can achieve logs like this:  

```log
「21:01:25.6 Info SomeSection」 This is a log
「21:01:25.6 Fatal SomeSection」 Another fatal log
```

## 更多内容 Additional Information
`Logger` 还可以接受一个 `Exception` 对象作为内容并可选地指定一个前缀 `prefix`.
实例请见 `Sample/Program.cs` 文件.  
The `Logger` can also accept an `Exception` object as the log content and optionally specify a `prefix`.
For examples, see the `Sample/Program.cs` file.