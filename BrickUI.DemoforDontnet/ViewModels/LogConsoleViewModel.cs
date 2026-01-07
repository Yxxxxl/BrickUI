using BrickUI.Controls.LogConsole;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BrickUI.DemoforDontnet.ViewModels
{
    public class LogConsoleViewModel : ObservableObject
    {
        private string _newMessage;

        public LogConsoleViewModel()
        {
            _newMessage = "Log console ready.";

            AddInfoCommand = new RelayCommand(() => AddEntry(LogSeverity.Info, _newMessage));
            AddWarningCommand = new RelayCommand(() => AddEntry(LogSeverity.Warning, _newMessage));
            AddErrorCommand = new RelayCommand(() => AddEntry(LogSeverity.Error, _newMessage));
            AddCustomCommand = new RelayCommand(AddCustom, CanAddCustom);
            ClearCommand = new RelayCommand(() => Entries.Clear());

            AddEntry(LogSeverity.Info, "Console initialized.");
        }

        public ObservableCollection<LogEntry> Entries { get; } = new ObservableCollection<LogEntry>();

        public string NewMessage
        {
            get => _newMessage;
            set
            {
                if (SetProperty(ref _newMessage, value))
                {
                    AddCustomCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public RelayCommand AddInfoCommand { get; }

        public RelayCommand AddWarningCommand { get; }

        public RelayCommand AddErrorCommand { get; }

        public RelayCommand AddCustomCommand { get; }

        public RelayCommand ClearCommand { get; }

        private void AddCustom()
        {
            AddEntry(LogSeverity.Debug, NewMessage);
        }

        private bool CanAddCustom()
        {
            return !string.IsNullOrWhiteSpace(NewMessage);
        }

        private void AddEntry(LogSeverity severity, string message)
        {
            var color = GetSeverityColor(severity);
            Entries.Add(new LogEntry(message, severity, color));
        }

        private static Brush GetSeverityColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Trace:
                    return Brushes.SlateGray;
                case LogSeverity.Debug:
                    return Brushes.DeepSkyBlue;
                case LogSeverity.Info:
                    return Brushes.MediumSeaGreen;
                case LogSeverity.Warning:
                    return Brushes.Goldenrod;
                case LogSeverity.Error:
                    return Brushes.OrangeRed;
                case LogSeverity.Critical:
                    return Brushes.HotPink;
                default:
                    return Brushes.Gainsboro;
            }
        }
    }
}

