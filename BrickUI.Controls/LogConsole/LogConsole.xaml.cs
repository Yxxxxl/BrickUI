using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BrickUI.Controls
{
    public partial class LogConsole : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(LogConsole),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty ClearCommandProperty =
            DependencyProperty.Register(nameof(ClearCommand), typeof(ICommand), typeof(LogConsole));

        public static readonly DependencyProperty AutoScrollProperty =
            DependencyProperty.Register(nameof(AutoScroll), typeof(bool), typeof(LogConsole),
                new PropertyMetadata(true));

        private INotifyCollectionChanged _notifySource;

        public LogConsole()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public ICommand ClearCommand
        {
            get => (ICommand)GetValue(ClearCommandProperty);
            set => SetValue(ClearCommandProperty, value);
        }

        public bool AutoScroll
        {
            get => (bool)GetValue(AutoScrollProperty);
            set => SetValue(AutoScrollProperty, value);
        }

        public void Clear()
        {
            if (ItemsSource is IList list)
            {
                list.Clear();
            }
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (LogConsole)d;
            control.DetachCollectionChanged(e.OldValue as INotifyCollectionChanged);
            control.AttachCollectionChanged(e.NewValue as INotifyCollectionChanged);
            control.ScrollToEnd();
        }

        private void AttachCollectionChanged(INotifyCollectionChanged source)
        {
            if (source == null)
            {
                return;
            }

            _notifySource = source;
            _notifySource.CollectionChanged += OnItemsCollectionChanged;
        }

        private void DetachCollectionChanged(INotifyCollectionChanged source)
        {
            if (source == null)
            {
                return;
            }

            source.CollectionChanged -= OnItemsCollectionChanged;
            if (ReferenceEquals(_notifySource, source))
            {
                _notifySource = null;
            }
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!AutoScroll)
            {
                return;
            }

            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Reset ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                ScrollToEnd();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ScrollToEnd();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DetachCollectionChanged(_notifySource);
        }

        private void ScrollToEnd()
        {
            if (!AutoScroll || PART_ScrollViewer == null)
            {
                return;
            }

            Dispatcher.BeginInvoke(new Action(() => PART_ScrollViewer.ScrollToEnd()), DispatcherPriority.Background);
        }

        private void OnClearClicked(object sender, RoutedEventArgs e)
        {
            if (ClearCommand != null && ClearCommand.CanExecute(null))
            {
                ClearCommand.Execute(null);
                return;
            }

            Clear();
        }
    }
}
