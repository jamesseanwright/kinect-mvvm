using System;
using System.Collections.Generic;
using WindowsPreview.Kinect;

namespace HelloKinect.Sensor
{
    public class Kinect : ISensor
    {
        KinectSensor sensor;
        MultiSourceFrameReader frameReader;
        
        public event EventHandler<IFrame> NewColourFrame;
        public event EventHandler<IFrame> NewInfraredFrame;
        public event EventHandler<IFrame> NewHeadFrame;


        public Kinect()
        {
            sensor = KinectSensor.GetDefault();
            frameReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            frameReader.MultiSourceFrameArrived += FrameArrived;
            sensor.Open();
        }

        private void RaiseColourFrame(byte[] data)
        {
            if (NewColourFrame != null)
            {
                NewColourFrame(this, new KinectFrame(data));
            }
        }

        private void RaiseInfraredFrame(byte[] data)
        {
            if (NewInfraredFrame != null)
            {
                NewInfraredFrame(this, new KinectFrame(data));
            }
        }

        private void RaiseHeadFrame(List<Tuple<double, double, double>> heads)
        {
            if (NewHeadFrame != null)
            {
                NewHeadFrame(this, new KinectFrame(heads));
            }
        }

        private void FrameArrived(MultiSourceFrameReader sender, MultiSourceFrameArrivedEventArgs args)
        {
            MultiSourceFrame multiSourceFrame = args.FrameReference.AcquireFrame();

            if (multiSourceFrame != null)
            {
                using (ColorFrame colourFrame = multiSourceFrame.ColorFrameReference.AcquireFrame())
                {
                    if (colourFrame != null)
                    {
                        byte[] colourData = new byte[colourFrame.FrameDescription.Width * colourFrame.FrameDescription.Height * 4];
                        colourFrame.CopyConvertedFrameDataToArray(colourData, ColorImageFormat.Bgra);
                        RaiseColourFrame(colourData);
                    }
                }

                using (BodyFrame bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
                        Body[] bodies = new Body[6];
                        List<Tuple<double, double, double>> heads = new List<Tuple<double, double, double>>();

                        bodyFrame.GetAndRefreshBodyData(bodies);

                        foreach (Body body in bodies)
                        {
                            Joint headJoint = body.Joints[JointType.Head];

                            if (headJoint.TrackingState == TrackingState.Tracked)
                            {
                                ColorSpacePoint spacePoint = sensor.CoordinateMapper.MapCameraPointToColorSpace(headJoint.Position);
                                heads.Add(new Tuple<double, double, double>(spacePoint.X, spacePoint.Y, headJoint.Position.Z));
                            }
                        }

                        RaiseHeadFrame(heads);
                    }
                }

                using (InfraredFrame irFrame = multiSourceFrame.InfraredFrameReference.AcquireFrame())
                {
                    if (irFrame != null)
                    {
                        ushort[] irData = new ushort[irFrame.FrameDescription.LengthInPixels];
                        byte[] irDataConverted = new byte[irFrame.FrameDescription.LengthInPixels * 4];

                        irFrame.CopyFrameDataToArray(irData);

                        for (int i = 0; i < irData.Length; i++)
                        {
                            byte intensity = (byte)(irData[i] >> 8);

                            irDataConverted[i * 4] = intensity;
                            irDataConverted[i * 4 + 1] = intensity;
                            irDataConverted[i * 4 + 2] = intensity;
                            irDataConverted[i * 4 + 3] = 255;
                        }

                        RaiseInfraredFrame(irDataConverted);
                        return;
                    }
                }
            }
        }
    }
}