using System;
using System.Windows.Media;

namespace BrickUI.Controls.LogConsole
{
    public sealed class LogEntry
    {
        public LogEntry(string message, LogSeverity severity, Brush foreground, DateTime? timestamp = null)
        {
            Message = message ?? string.Empty;
            Severity = severity;
            Foreground = foreground ?? Brushes.White;
            Timestamp = timestamp ?? DateTime.Now;
        }

        public string Message { get; }

        public LogSeverity Severity { get; }

        public Brush Foreground { get; }

        public DateTime Timestamp { get; }
    }
}
