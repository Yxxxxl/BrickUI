using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BrickUI.Controls.SideMenu
{
    public partial class SideMenu : UserControl
    {
        private const int AnimationDurationMs = 360;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(SideMenu));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(SideMenu));

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(SideMenu),
                new PropertyMetadata(true, OnMenuSizeChanged));

        public static readonly DependencyProperty ExpandedWidthProperty =
            DependencyProperty.Register(nameof(ExpandedWidth), typeof(double), typeof(SideMenu),
                new PropertyMetadata(220d, OnMenuSizeChanged));

        public static readonly DependencyProperty CollapsedWidthProperty =
            DependencyProperty.Register(nameof(CollapsedWidth), typeof(double), typeof(SideMenu),
                new PropertyMetadata(0d, OnMenuSizeChanged));

        public static readonly DependencyProperty AnimatedMenuWidthProperty =
            DependencyProperty.Register(nameof(AnimatedMenuWidth), typeof(double), typeof(SideMenu),
                new PropertyMetadata(220d));

        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.Register(nameof(RightContent), typeof(object), typeof(SideMenu));

        public static readonly DependencyProperty ToggleBackgroundCollapsedProperty =
            DependencyProperty.Register(nameof(ToggleBackgroundCollapsed), typeof(System.Windows.Media.Brush), typeof(SideMenu),
                new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0x0F, 0x17, 0x2A)), OnToggleStyleChanged));

        public static readonly DependencyProperty ToggleBackgroundExpandedProperty =
            DependencyProperty.Register(nameof(ToggleBackgroundExpanded), typeof(System.Windows.Media.Brush), typeof(SideMenu),
                new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0x0B, 0x12, 0x20)), OnToggleStyleChanged));

        public static readonly DependencyProperty ToggleForegroundCollapsedProperty =
            DependencyProperty.Register(nameof(ToggleForegroundCollapsed), typeof(System.Windows.Media.Brush), typeof(SideMenu),
                new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0x0F, 0x17, 0x2A)), OnToggleStyleChanged));

        public static readonly DependencyProperty ToggleForegroundExpandedProperty =
            DependencyProperty.Register(nameof(ToggleForegroundExpanded), typeof(System.Windows.Media.Brush), typeof(SideMenu),
                new PropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0xE2, 0xE8, 0xF0)), OnToggleStyleChanged));

        public static readonly DependencyProperty ToggleBackgroundActiveProperty =
            DependencyProperty.Register(nameof(ToggleBackgroundActive), typeof(System.Windows.Media.Brush), typeof(SideMenu));

        public static readonly DependencyProperty ToggleForegroundActiveProperty =
            DependencyProperty.Register(nameof(ToggleForegroundActive), typeof(System.Windows.Media.Brush), typeof(SideMenu));

        public SideMenu()
        {
            InitializeComponent();
            AnimatedMenuWidth = ExpandedWidth;
            Loaded += OnLoaded;
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public double ExpandedWidth
        {
            get => (double)GetValue(ExpandedWidthProperty);
            set => SetValue(ExpandedWidthProperty, value);
        }

        public double CollapsedWidth
        {
            get => (double)GetValue(CollapsedWidthProperty);
            set => SetValue(CollapsedWidthProperty, value);
        }

        public double AnimatedMenuWidth
        {
            get => (double)GetValue(AnimatedMenuWidthProperty);
            private set => SetValue(AnimatedMenuWidthProperty, value);
        }

        public object RightContent
        {
            get => GetValue(RightContentProperty);
            set => SetValue(RightContentProperty, value);
        }

        public System.Windows.Media.Brush ToggleBackgroundCollapsed
        {
            get => (System.Windows.Media.Brush)GetValue(ToggleBackgroundCollapsedProperty);
            set => SetValue(ToggleBackgroundCollapsedProperty, value);
        }

        public System.Windows.Media.Brush ToggleBackgroundExpanded
        {
            get => (System.Windows.Media.Brush)GetValue(ToggleBackgroundExpandedProperty);
            set => SetValue(ToggleBackgroundExpandedProperty, value);
        }

        public System.Windows.Media.Brush ToggleForegroundCollapsed
        {
            get => (System.Windows.Media.Brush)GetValue(ToggleForegroundCollapsedProperty);
            set => SetValue(ToggleForegroundCollapsedProperty, value);
        }

        public System.Windows.Media.Brush ToggleForegroundExpanded
        {
            get => (System.Windows.Media.Brush)GetValue(ToggleForegroundExpandedProperty);
            set => SetValue(ToggleForegroundExpandedProperty, value);
        }

        public System.Windows.Media.Brush ToggleBackgroundActive
        {
            get => (System.Windows.Media.Brush)GetValue(ToggleBackgroundActiveProperty);
            private set => SetValue(ToggleBackgroundActiveProperty, value);
        }

        public System.Windows.Media.Brush ToggleForegroundActive
        {
            get => (System.Windows.Media.Brush)GetValue(ToggleForegroundActiveProperty);
            private set => SetValue(ToggleForegroundActiveProperty, value);
        }

        private static void OnMenuSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SideMenu)d;
            control.UpdateMenuWidth();
            control.UpdateToggleColors(true);
        }

        private static void OnToggleStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SideMenu)d;
            control.UpdateToggleColors(false);
        }

        private void UpdateMenuWidth()
        {
            var width = IsExpanded ? ExpandedWidth : CollapsedWidth;
            if (width < 0)
            {
                width = 0;
            }

            AnimateMenuWidth(width);
        }

        private void AnimateMenuWidth(double width)
        {
            var animation = new System.Windows.Media.Animation.DoubleAnimation
            {
                To = width,
                Duration = TimeSpan.FromMilliseconds(360),
                EasingFunction = new System.Windows.Media.Animation.CubicEase
                {
                    EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut
                }
            };

            BeginAnimation(AnimatedMenuWidthProperty, animation);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateToggleColors(false);
        }

        private void UpdateToggleColors(bool animate)
        {
            var targetBackground = IsExpanded ? ToggleBackgroundExpanded : ToggleBackgroundCollapsed;
            var targetForeground = IsExpanded ? ToggleForegroundExpanded : ToggleForegroundCollapsed;

            ToggleBackgroundActive = EnsureBrush(ToggleBackgroundActive, targetBackground, animate);
            ToggleForegroundActive = EnsureBrush(ToggleForegroundActive, targetForeground, animate);
        }

        private System.Windows.Media.Brush EnsureBrush(System.Windows.Media.Brush active, System.Windows.Media.Brush target, bool animate)
        {
            var activeSolid = active as System.Windows.Media.SolidColorBrush;
            var targetSolid = target as System.Windows.Media.SolidColorBrush;
            if (targetSolid == null)
            {
                return target;
            }

            if (activeSolid == null)
            {
                activeSolid = new System.Windows.Media.SolidColorBrush(targetSolid.Color);
            }

            if (!animate)
            {
                activeSolid.Color = targetSolid.Color;
                return activeSolid;
            }

            var animation = new System.Windows.Media.Animation.ColorAnimation
            {
                To = targetSolid.Color,
                Duration = TimeSpan.FromMilliseconds(AnimationDurationMs),
                EasingFunction = new System.Windows.Media.Animation.CubicEase
                {
                    EasingMode = System.Windows.Media.Animation.EasingMode.EaseInOut
                }
            };

            activeSolid.BeginAnimation(System.Windows.Media.SolidColorBrush.ColorProperty, animation);
            return activeSolid;
        }

        private void OnItemMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ListBoxItem itemContainer))
            {
                return;
            }

            if (!(itemContainer.DataContext is SideMenuItem item))
            {
                return;
            }

            SelectedItem = item;
            ExecuteItemCommand(item);
        }

        private static void ExecuteItemCommand(SideMenuItem item)
        {
            if (item == null || !item.IsEnabled)
            {
                return;
            }

            var command = item.Command;
            if (command == null)
            {
                return;
            }

            var parameter = item.CommandParameter ?? item;
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }
    }
}
