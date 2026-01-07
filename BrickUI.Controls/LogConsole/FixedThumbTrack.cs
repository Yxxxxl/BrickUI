using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace BrickUI.Controls
{
    public class FixedThumbTrack : Track
    {
        public static readonly DependencyProperty ThumbLengthProperty =
            DependencyProperty.Register(nameof(ThumbLength), typeof(double), typeof(FixedThumbTrack),
                new PropertyMetadata(28d, OnThumbLengthChanged));

        public double ThumbLength
        {
            get => (double)GetValue(ThumbLengthProperty);
            set => SetValue(ThumbLengthProperty, value);
        }

        private static void OnThumbLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var track = (FixedThumbTrack)d;
            track.InvalidateArrange();
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (Thumb == null || DecreaseRepeatButton == null || IncreaseRepeatButton == null)
            {
                return arrangeSize;
            }

            var vertical = Orientation == Orientation.Vertical;
            var trackLength = vertical ? arrangeSize.Height : arrangeSize.Width;
            var thumbLength = ThumbLength;
            if (thumbLength <= 0)
            {
                thumbLength = vertical ? Thumb.DesiredSize.Height : Thumb.DesiredSize.Width;
            }

            if (thumbLength <= 0)
            {
                thumbLength = 28d;
            }

            if (thumbLength > trackLength)
            {
                thumbLength = trackLength;
            }

            var range = Maximum - Minimum;
            var ratio = range <= 0 ? 0 : (Value - Minimum) / range;
            ratio = Math.Max(0, Math.Min(1, ratio));
            if (IsDirectionReversed)
            {
                ratio = 1 - ratio;
            }

            var available = trackLength - thumbLength;
            if (available < 0)
            {
                available = 0;
            }

            var thumbOffset = available * ratio;
            if (vertical)
            {
                DecreaseRepeatButton.Arrange(new Rect(0, 0, arrangeSize.Width, thumbOffset));
                Thumb.Arrange(new Rect(0, thumbOffset, arrangeSize.Width, thumbLength));
                IncreaseRepeatButton.Arrange(new Rect(0, thumbOffset + thumbLength, arrangeSize.Width, trackLength - thumbOffset - thumbLength));
            }
            else
            {
                DecreaseRepeatButton.Arrange(new Rect(0, 0, thumbOffset, arrangeSize.Height));
                Thumb.Arrange(new Rect(thumbOffset, 0, thumbLength, arrangeSize.Height));
                IncreaseRepeatButton.Arrange(new Rect(thumbOffset + thumbLength, 0, trackLength - thumbOffset - thumbLength, arrangeSize.Height));
            }

            return arrangeSize;
        }
    }
}
