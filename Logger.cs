using System;
using System.Diagnostics;

namespace pasLogger
{
    public enum LogType { ltInfo, ltWarning, ltError }
    public delegate void LogEvent(LogType ALogtype, string Message);

    public static class Logger
    {
        // Private
        private static LogEvent FLogEvent;

        // Public

        public static LogEvent logEvent { get { return FLogEvent; } set { FLogEvent = value; } }

        public static void Log(LogType ALogtype, string Message)
        {
            FLogEvent?.Invoke(ALogtype, Message);
        }

    }
}
