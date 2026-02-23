using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ContentSizeTransitionDemo.Components
{
    public class ContentSizeTransition : ContentControl
    {
        private Size _targetMeasureSize;
        private static readonly IEasingFunction _easing = new SineEase(){EasingMode = EasingMode.EaseOut};

        private Size MeasureSizeAnimated
        {
            get { return (Size)GetValue(SizeOverrideProperty); }
            set { SetValue(SizeOverrideProperty, value); }
        }

        private static readonly DependencyProperty SizeOverrideProperty =
            DependencyProperty.Register(nameof(MeasureSizeAnimated), typeof(Size), typeof(ContentSizeTransition),
                new FrameworkPropertyMetadata(default(Size), FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size MeasureOverride(Size constraint)
        {
            var measureSizeAnimated = MeasureSizeAnimated;
            var currentMeasureSize = base.MeasureOverride(constraint);

            if (currentMeasureSize != _targetMeasureSize)
            {
                BeginAnimation(SizeOverrideProperty, null);
                BeginAnimation(SizeOverrideProperty, new SizeAnimation
                {
                    From = DesiredSize,
                    To = currentMeasureSize,
                    Duration = TimeSpan.FromSeconds(0.15),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = _easing
                });

                _targetMeasureSize = currentMeasureSize;
                return DesiredSize;
            }

            return measureSizeAnimated;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (VisualChildrenCount > 0 &&
                GetVisualChild(0) is UIElement elementChild)
            {
                var childRect = new Rect(0, 0, arrangeBounds.Width, arrangeBounds.Height);
                elementChild.Arrange(childRect);
            }

            return arrangeBounds;
        }
    }
}
