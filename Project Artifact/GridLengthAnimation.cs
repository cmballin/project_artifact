using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Project_Artifact
{


    public delegate void ChangeDelegate(double percentage);

    public class GridLengthAnimation : AnimationTimeline
    {
        double currentProgress;

        protected GridLengthAnimation first;


        public event ChangeDelegate changeEvent;





        public GridLengthAnimation(GridLengthAnimation parent = null)
        {

            first = parent;
            // no-op
        }

        private void raiseChangeEvent(double percentage)
        {
            if (first == null)
                return;
            if (first.changeEvent == null)
                return;

            first.changeEvent(percentage);
        }


        public GridLength From
        {
            get { return (GridLength)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
          DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));

        public GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));

        public override Type TargetPropertyType
        {
            get { return typeof(GridLength); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation(this);
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromValue = this.From.Value;
            double toValue = this.To.Value;
            currentProgress = animationClock.CurrentProgress.Value;
            raiseChangeEvent(currentProgress * 100);

            if (fromValue > toValue)
            {
                return new GridLength((1 - animationClock.CurrentProgress.Value) * (fromValue - toValue) + toValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
            else
            {
                return new GridLength((animationClock.CurrentProgress.Value) * (toValue - fromValue) + fromValue, this.To.IsStar ? GridUnitType.Star : GridUnitType.Pixel);
            }
        }
    }
}