using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using WindowsPreview.Kinect;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using HelloKinect.Sensor;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HelloKinect.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //KinectSensor sensor;
        //ColorFrameReader colourReader;
        //InfraredFrameReader irReader;
        //ushort[] irData;
        //byte[] irDataConverted;

        //Body[] bodies;
        //MultiSourceFrameReader msfr;

        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel(new KinectSensorService());
            //Loaded += MainPageLoaded;
        }

        void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        //private void FrameArrived(MultiSourceFrameReader sender, MultiSourceFrameArrivedEventArgs args)
        //{
        //    using (MultiSourceFrame multiSourceFrame = args.FrameReference.AcquireFrame())
        //    {
        //        if (multiSourceFrame != null)
        //        {
        //            using (ColorFrame colourFrame = multiSourceFrame.ColorFrameReference.AcquireFrame())
        //            {
        //                if (colourFrame != null)
        //                {
        //                    colourFrame.CopyConvertedFrameDataToBuffer(colourBitmap.PixelBuffer, ColorImageFormat.Bgra);
        //                    colourBitmap.Invalidate();
        //                }
        //            }

        //            using (BodyFrame bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame())
        //            {
        //                if (bodyFrame != null)
        //                {
        //                    bodyOutput.Children.Clear();
        //                    bodyFrame.GetAndRefreshBodyData(bodies);

        //                    foreach (Body body in bodies)
        //                    {
        //                        if (body.IsTracked)
        //                        {
        //                            Joint headJoint = body.Joints[JointType.Head];

        //                            if (headJoint.TrackingState == TrackingState.Tracked)
        //                            {
        //                                DepthSpacePoint dsp = sensor.CoordinateMapper.MapCameraPointToDepthSpace(headJoint.Position);
        //                                Ellipse headCircle = new Ellipse() { Width = 50, Height = 50, Fill = new SolidColorBrush(Colors.Yellow) };
        //                                bodyOutput.Children.Add(headCircle);
        //                                Canvas.SetLeft(headCircle, (dsp.X - 25) / 2);
        //                                Canvas.SetTop(headCircle, (dsp.Y - 25) / 2);
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            using (InfraredFrame irFrame = multiSourceFrame.InfraredFrameReference.AcquireFrame())
        //            {
        //                if (irFrame != null)
        //                {
        //                    irFrame.CopyFrameDataToArray(irData);

        //                    for (int i = 0; i < irData.Length; i++)
        //                    {
        //                        byte intensity = (byte)(irData[i] >> 8);

        //                        irDataConverted[i * 4] = intensity;
        //                        irDataConverted[i * 4 + 1] = intensity;
        //                        irDataConverted[i * 4 + 2] = intensity;
        //                        irDataConverted[i * 4 + 3] = 255;
        //                    }

        //                    irDataConverted.CopyTo(irBitmap.PixelBuffer);
        //                    irBitmap.Invalidate();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
