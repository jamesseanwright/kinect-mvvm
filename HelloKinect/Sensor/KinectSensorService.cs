using System;
using WindowsPreview.Kinect;

namespace HelloKinect.Sensor
{
    public class KinectSensorService : ISensorService
    {
        KinectSensor sensor;
        MultiSourceFrameReader frameReader;
        
        public event EventHandler<IFrame> NewColourFrame;
        public event EventHandler<IFrame> NewInfraredFrame;
        public event EventHandler<IFrame> NewHeadFrame;


        public KinectSensorService()
        {
            sensor = KinectSensor.GetDefault();
            sensor.InfraredFrameSource.OpenReader();
            frameReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.Color | FrameSourceTypes.Infrared);
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

        private void RaiseHeadFrame(float[] points)
        {
            if (NewHeadFrame != null)
            {
                NewHeadFrame(this, new KinectFrame(points));
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
                        byte[] colourData = new byte[8294400];
                        colourFrame.CopyConvertedFrameDataToArray(colourData, ColorImageFormat.Bgra);
                        RaiseColourFrame(colourData);
                        return;
                    }
                }

                using (BodyFrame bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
                        Body[] bodies = new Body[6];
                        bodyFrame.GetAndRefreshBodyData(bodies);

                        if (bodies.Length < 1 || !bodies[0].IsTracked) {
                            return;
                        }

                        Body body = bodies[0];

                        Joint headJoint = body.Joints[JointType.Head];

                        if (headJoint.TrackingState == TrackingState.NotTracked)
                        {
                            return;
                        }

                        DepthSpacePoint headPosition = sensor.CoordinateMapper.MapCameraPointToDepthSpace(headJoint.Position);

                        RaiseHeadFrame(new float[] { headPosition.X, headPosition.Y });
                        return;
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
