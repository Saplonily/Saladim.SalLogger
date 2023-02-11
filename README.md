# Saladim.SalLogger

<div align="middle">
    一个被建议用于游戏/小框架的轻量级日志框架
</div>

## 引用
你可以到nuget上搜索SalLogger以nuget包形式安装, 或者直接引入项目全代码(在[gist](https://gist.github.com/Saplonily/2b23e2cfa00223fa27c483af098d27e1)中)

## 构建 Build
本项目使用Visual Studio 2022创建,面向`.net standard2.1`,`.net core 3.1`,`.net 6.0`  ,`.net standard2.0`
使用vs2022的默认构建方法即可

## 使用 Usage
核心类为`Logger`,位于`Saladim.SalLogger`命名空间下, 
在开始记录日志前应该创建同命名空间的`LoggerBuilder`建造器,例如:
```C#
LoggerBuilder builder = new();
builder.WithLevelLimit(LogLevel.Trace)
       .WithLogToConsole();
```
`WithLevelLimit`限制了将被建造了logger真正输出日志的最低等级,日志等级(`LogLevel`)分为:
- Trace
- Debug
- Info
- Warn
- Error
- Fatal

如果将限制等级设置为`Info`,那么`Debug`与`Trace`将不会被记录  
`WithLogToConsole`指示建造器应该将这个输出到控制台上  
配置完建造器后可使用建造器的`Build`方法获取一个`Logger`对象:
```C#
Logger logger = builder.Build();
```
`Logger`对象有许多重载方法,你可以选择适合情景的重载版本  
例如如下实例:
```C#
logger.Log(LogLevel.Info, "sectionName", "Your log message.");
logger.Log(LogLevel.Warn, "sectionName", "subSectionName", "Your log message.");
logger.LogInfo("sectionName", "easier to log info message.");
logger.LogFatal("sectionName", "subSection", "you can also have `subSection` in easier way.");
logger.LogTrace("sectionName", "this log message will not be logged because we limit the `LevelLimit` to `Info`");
```
将会产生如下输出(控制台):
```log
[20:56:33.2] [Info:sectionName]: Your log message.
[20:56:33.3] [Warn:sectionName/subSectionName]: Your log message.
[20:56:33.3] [Info:sectionName]: easier to log info message.
[20:56:33.3] [Fatal:sectionName/subSection]: you can also have `subSection` in easier way.
```

## 更多实例
如果你不满足于目前的日志格式,那么你可以使用建造器的`WithFormatter`方法指定格式化器,
该方法接受一个LogFormatter委托实例,原型如下:  
```C#
public delegate string LogFormatter(LogLevel logLevel,string section,string? subSection,string content);
```
特别注意,当未指定`subSection`时会将`null`传入  
例如使用如下格式化器实例:
```C#
static string MyFormatter(LogLevel logLevel, string section, string subSection, string content)
    => $"「{DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.f} {logLevel} {section}" +
    $"{(subSection is null ? "" : $"/{subSection}")}」 {content}";
```
会产生如下所示的日志效果:  
```log
「21:01:25.6 Info SomeSection」 这是一条log
「21:01:25.6 Fatal SomeSection」 另一条fatal log
```

## 更多内容
`Logger`接受一个`Exception`对象作为内容,同时加入前缀和后缀
实例请见`Sample/Program.cs`文件
