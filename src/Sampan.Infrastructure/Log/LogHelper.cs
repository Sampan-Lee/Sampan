using NLog;
using System;

namespace Sampan.Common.Log
{
    public static class LogHelper
    {
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region LogTrace
        public static void Trace(string message, LogContent logContent)
        {
            new LogEventInfo() { Level = LogLevel.Trace, Message = message }.SetLogEventInfoProperties(logContent).ToLog();
        }
        public static void Trace(string message)
        {
            logger.Trace(message);
        }

        public static void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }
        #endregion

        #region LogDebug

        public static void Debug(string message, LogContent logContent)
        {
            new LogEventInfo() { Level = LogLevel.Debug, Message = message }.SetLogEventInfoProperties(logContent).ToLog();
        }
        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        #endregion

        #region LogError

        public static void Error(string message, LogContent logContent)
        {
            new LogEventInfo() { Level = LogLevel.Error, Message = message }.SetLogEventInfoProperties(logContent).ToLog();
        }
        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public static void Error(Exception ex, string message,params object[] args)
        {
            logger.Error(ex, message, args);
        }

        #endregion

        #region Info

        public static void Info(string message, LogContent logContent)
        {
            new LogEventInfo() { Level = LogLevel.Info, Message = message }.SetLogEventInfoProperties(logContent).ToLog();
        }
        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        #endregion

        #region LogWarning

        public static void Warning(string message, LogContent logContent)
        {
            new LogEventInfo() { Level = LogLevel.Warn, Message = message }.SetLogEventInfoProperties(logContent).ToLog();
        }
        public static void Warning(string message)
        {
            logger.Warn(message);
        }

        public static void Warning(string message, params object[] args)
        {
            logger.Warn(message, args);
        }
        #endregion

        #region LogFatal

        public static void Fatal(string message, LogContent logContent)
        {
            new LogEventInfo() { Level = LogLevel.Fatal, Message = message }.SetLogEventInfoProperties(logContent).ToLog();
        }
        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public static void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        #endregion

        /// <summary>
        /// 指定字段字段存储 logContent 模板对象
        /// </summary>
        /// <param name="logEventInfo"></param>
        /// <param name="logContent"></param>
        /// <returns></returns>
        public static LogEventInfo SetLogEventInfoProperties(this LogEventInfo logEventInfo, LogContent logContent)
        {

            if (logContent == null) return logEventInfo;

            foreach (System.Reflection.PropertyInfo p in logContent.GetType().GetProperties())
            {
                logEventInfo.Properties[p.Name] = p.GetValue(logContent);
                Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(logContent));
            }
            return logEventInfo;
        }

        public static void ToLog(this LogEventInfo logEvent)
        {
            logger?.Log(logEvent);
        }
    }

    #region LogContent 日志模板

    public class LogContent
    {
        public LogContent()
        {
        }
        public LogContent(string message)
        {
            UserId = message;
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        public string UserId { get; set; }
    }

    #endregion

    #region 日志枚举

    public enum NLogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
        Off,
    }
    #endregion
}
