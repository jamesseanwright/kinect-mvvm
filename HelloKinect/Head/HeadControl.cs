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
        const float BaseWidth = 200;
        const float BaseHeight = 200;
        const float DepthSpaceWidth = 512;
        const float DepthSpaceHeight = 424;

        Shape headPresenter;

        public HeadControl()
        {
            this.DefaultStyleKey = typeof (HeadControl);
        }

        public static readonly DependencyProperty XProperty
            = DependencyProperty.Register("X", typeof (float), typeof (HeadControl), new PropertyMetadata(default (float), PositionUpdated));

        public float X
        {
            get
            {
                return (float) GetValue(XProperty);
            }

            set
            {
                SetValue(XProperty, value);
            }
        }

        public static readonly DependencyProperty YProperty
            = DependencyProperty.Register("Y", typeof(float), typeof(HeadControl), new PropertyMetadata(default(float), PositionUpdated));

        public float Y
        {
            get
            {
                return (float) GetValue(YProperty);
            }

            set
            {
                SetValue(YProperty, value);
            }
        }

        public static readonly DependencyProperty ZProperty
            = DependencyProperty.Register("Y", typeof(float), typeof(HeadControl), new PropertyMetadata(default(float), PositionUpdated));

        public float Z
        {
            get
            {
                return (float)GetValue(ZProperty);
            }

            set
            {
                SetValue(ZProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            this.headPresenter = GetTemplateChild("PART_headPresenter") as Shape;

            if (this.headPresenter == null)
            {
                return;
            }

            this.headPresenter.Width = BaseWidth;
            this.headPresenter.Height = BaseHeight;
        }

        private void Transform()
        {
            if (this.headPresenter == null)
            {
                return;
            }

            Rect window = Window.Current.Bounds;
            Canvas.SetLeft(this.headPresenter, (X * (window.Width / DepthSpaceWidth)) - (this.headPresenter.Width / 2));
            Canvas.SetTop(this.headPresenter, (Y * (window.Height / DepthSpaceHeight)) - this.headPresenter.Height / 2);
        }

        private static void PositionUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HeadControl) d).Transform();
        }
    }
}
