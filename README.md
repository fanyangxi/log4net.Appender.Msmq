log4net.Appender.Msmq
=====================
This is a log4net appender for MSMQ (Microsoft Message Queuing)

[![NuGet version](https://badge.fury.io/nu/log4net.appender.msmq.svg)](https://badge.fury.io/nu/log4net.appender.msmq)

## Getting started

Built with Visual Studio 2013, .Net framework 4.0

### General explaination:
You can use this appender to write 

### Configuration sample, to write info to MSMQ:
```xml
<appender name="Msmq" type="log4net.Appender.Msmq.MsmqAppender, log4net.Appender.Msmq">
  <QueueName>.\private$\log-test</QueueName>
  <labelLayout value="LOG [%level] %date"/>
  <threshold value="DEBUG" />
  <layout type="log4net.Layout.PatternLayout">
    <conversionPattern value="%message" />
  </layout>
</appender>

Other sample log4net conversion parttern:
<conversionPattern value="%date [%thread] %-5level %logger (%property{log4net:HostName}) [%ndc] - %message %exception %newline" />
<conversionPattern value="{'date': '%date', 'level': '%level', 'thread': '%thread', 'logger': '%logger', 'message': '%message', 'exception': '%exception'} %n" />
```

### Sample code to read string from MSMQ:
```C#

```

## License

https://github.com/fanyangxi/log4net.Appender.Msmq/blob/master/LICENSE