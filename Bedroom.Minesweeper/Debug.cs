using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Bedroom.Minesweeper
{
    public static class Debug
    {
        #region Private Fields

        private const string ERROR_STRING = "ERROR";
        private const string EXCEPTION_STRING = "EXCEPTION";
        private const string INFO_STRING = "INFO";
        private const string LOG_FILE = "log.txt";
        private const string WARNING_STRING = "WARNING";

        internal static void Setup()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            severityPrefix = new Dictionary<LogSeverity, string>()
            {
                { LogSeverity.Error, $"[{ERROR_STRING}] " },
                { LogSeverity.Exception, $"[{EXCEPTION_STRING}] " },
                { LogSeverity.Info, $"[{INFO_STRING}] " },
                { LogSeverity.Warning, $"[{WARNING_STRING}] " }
            };

            severityColor = new Dictionary<LogSeverity, ConsoleColor>()
            {
                { LogSeverity.Error, ConsoleColor.Red },
                { LogSeverity.Exception, ConsoleColor.DarkRed },
                { LogSeverity.Info, ConsoleColor.Gray },
                { LogSeverity.Warning, ConsoleColor.Yellow }
            };
            // The default severity is "show everything"
            MinSeverity = LogSeverity.Info;
            // The stringbuilder for creating the message
            stringBuilder = new StringBuilder();
            // We use the German culture for datetime, because it spits out hh:mm:ss (like any sane being)
            dateTimeCulture = CultureInfo.CreateSpecificCulture("de-DE");

            // Enable all of the settings by default
            UseLogFile = true;
            ShowTimeStamp = true;
            UseColors = true;

            logFilePath = Path.Combine(AppData.DataFolder, LOG_FILE);
            if (File.Exists(logFilePath))
                File.Delete(logFilePath);
            File.Create(logFilePath).Dispose(); // We immediatly dispose the returned stream, to free the lock on the file

            CommandLineArguments.Load();

            // We call it here, because now we can't have a cyclic reference anymore (all other variables are set-up)
            if (CommandLineArguments.NoLog)
                UseLogFile = false;
        }

        private static IFormatProvider dateTimeCulture;
        private static string logFilePath;
        private static Dictionary<LogSeverity, ConsoleColor> severityColor;
        private static Dictionary<LogSeverity, string> severityPrefix;
        private static StringBuilder stringBuilder;

        #endregion Private Fields

        #region Public Enums

        /// <summary>
        /// The severity of the log message. Can be used to filter for specific messages.
        /// </summary>
        public enum LogSeverity
        {
            Info,
            Warning,
            Error,
            Exception
        }

        #endregion Public Enums

        #region Public Properties

        /// <summary>
        /// The minimal severity for logs. Logs of severity below this, will not be displayed in the
        /// log file or the console.
        /// </summary>
        public static LogSeverity MinSeverity { get; set; }

        /// <summary>
        /// If true, the logger will show the timestamp for each message
        /// </summary>
        public static bool ShowTimeStamp { get; set; }

        /// <summary>
        /// If true, the logger will fill the log file
        /// </summary>
        public static bool UseLogFile { get; set; }

        /// <summary>
        /// If true, the different severities will be shown in different colors
        /// </summary>
        public static bool UseColors { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Log a message with the info prefix
        /// </summary>
        /// <param name="text">The message to send</param>
        public static void Log(string text)
        {
            LogInternal(text, LogSeverity.Info);
        }

        /// <summary>
        /// Log a message with the error prefix
        /// </summary>
        /// <param name="text">The message to send</param>
        public static void LogError(string text)
        {
            LogInternal(text, LogSeverity.Error);
        }

        /// <summary>
        /// Log an exception that occured. This will be autologged on all uncaught exceptions
        /// </summary>
        /// <param name="exception">The exception object</param>
        public static void LogException(object exception)
        {
            LogInternal(exception.ToString(), LogSeverity.Exception);
        }

        /// <summary>
        /// Log a message with the warning prefix
        /// </summary>
        /// <param name="text">The message to send</param>
        public static void LogWarning(string text)
        {
            LogInternal(text, LogSeverity.Warning);
        }

        #endregion Public Methods

        #region Private Methods

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogException(e.ExceptionObject);
        }

        private static void LogInternal(string text, LogSeverity severity)
        {
            if (severity < MinSeverity)
                return;

            // Let's allocate the required buffer with a tiny bit of extra space
            int targetLength = severityPrefix[severity].Length + text.Length + (ShowTimeStamp ? 10 : 0) + 4;

            stringBuilder.Clear();
            stringBuilder.EnsureCapacity(targetLength);
            if (ShowTimeStamp)
                stringBuilder.Append($"[{DateTime.Now.ToString("T", dateTimeCulture)}]"); // Append in the format [hh:mm:ss]
            stringBuilder.Append(severityPrefix[severity]); // Append in the format [{SeverityType}]
            stringBuilder.AppendLine(text); // Append the logtext itself

#if DEBUG
            // Will not work in visual studio, the debug output is redirected to the vs out, which does not support colors
            if (UseColors)
                Console.ForegroundColor = severityColor[severity];
            Console.Write(stringBuilder.ToString()); // Write to stdout
#endif

            if (UseLogFile)
                File.AppendAllText(logFilePath, stringBuilder.ToString());
        }

        #endregion Private Methods
    }
}