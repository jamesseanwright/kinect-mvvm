using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace HelloKinect.Head
{
    [TemplatePart(Name = "PART_headPresenter", Type = typeof (Control))]
    public sealed class HeadControl : Control
    {
        const double BasePresenterWidth = 180;
        const double BasePresenterHeight = 180;
        const double DepthSpaceWidth = 512;
        const double DepthSpaceHeight = 424;

        FrameworkElement headPresenter;

        public HeadControl()
        {
            this.DefaultStyleKey = typeof (HeadControl);
        }

        public static readonly DependencyProperty XProperty
            = DependencyProperty.Register("X", typeof(double), typeof(HeadControl), new PropertyMetadata(default(double), PositionUpdated));

        public double X
        {
            get
            {
                return (double)GetValue(XProperty);
            }

            set
            {
                SetValue(XProperty, value);
            }
        }

        public static readonly DependencyProperty YProperty
            = DependencyProperty.Register("Y", typeof(double), typeof(HeadControl), new PropertyMetadata(default(double)));

        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }

            set
            {
                SetValue(YProperty, value);
            }
        }

        public static readonly DependencyProperty ZProperty
            = DependencyProperty.Register("Z", typeof(double), typeof(HeadControl), new PropertyMetadata(default(double)));

        public double Z
        {
            get
            {
                return (double)GetValue(ZProperty);
            }

            set
            {
                SetValue(ZProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            this.headPresenter = GetTemplateChild("PART_headPresenter") as FrameworkElement;

            if (this.headPresenter == null)
            {
                return;
            }

            this.headPresenter.Width = BasePresenterWidth;
            this.headPresenter.Height = BasePresenterHeight;
        }

        private void Transform()
        {
            if (this.headPresenter == null)
            {
                return;
            }

            if (Z > 0)
            {
                this.headPresenter.Width = (BasePresenterWidth - (Z * 50)) * 4;
                this.headPresenter.Height = (BasePresenterHeight - (Z * 50)) * 4;
            }

            Rect window = Window.Current.Bounds;
            Canvas.SetLeft(this.headPresenter, (X * (window.Width / DepthSpaceWidth)) - (this.headPresenter.Width / 2));
            Canvas.SetTop(this.headPresenter, (Y * (window.Height / DepthSpaceHeight)) - (this.headPresenter.Height / 2));
        }

        private static void PositionUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HeadControl) d).Transform();
        }
    }
}
