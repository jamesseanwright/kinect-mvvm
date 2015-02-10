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

namespace KinectMvvm.Head
{
    [TemplatePart(Name = "PART_headPresenter", Type = typeof (Control))]
    public sealed class HeadControl : Control
    {
        const double DepthSpaceWidth = 1920;
        const double DepthSpaceHeight = 1080;

        Rect bounds = Window.Current.Bounds;
        double basePresenterWidth;
        double basePresenterHeight;

        FrameworkElement headPresenter;

        private void UpdateBaseHeadSize()
        {
            this.bounds = Window.Current.Bounds;
            this.basePresenterWidth = 180 * bounds.Width / DepthSpaceWidth;
            this.basePresenterHeight = 180 * bounds.Height / DepthSpaceHeight;
        }

        public HeadControl()
        {
            this.DefaultStyleKey = typeof (HeadControl);
            UpdateBaseHeadSize();
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

            this.headPresenter.Width = basePresenterWidth;
            this.headPresenter.Height = basePresenterHeight;
        }

        private void Transform()
        {
            if (this.headPresenter == null)
            {
                return;
            }

            UpdateBaseHeadSize();

            if (Z > 0)
            {
                this.headPresenter.Width = (basePresenterWidth - (Z * 50)) * 4;
                this.headPresenter.Height = (basePresenterHeight - (Z * 50)) * 4;
            }

            bounds = Window.Current.Bounds;

            Canvas.SetLeft(this.headPresenter, (X * bounds.Width / DepthSpaceWidth) - (this.headPresenter.Width / 2));
            Canvas.SetTop(this.headPresenter, (Y * bounds.Height / DepthSpaceHeight) - (this.headPresenter.Height / 2));
        }

        private static void PositionUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HeadControl) d).Transform();
        }
    }
}
