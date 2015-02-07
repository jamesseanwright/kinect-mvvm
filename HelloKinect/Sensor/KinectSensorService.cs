using System;
using WindowsPreview.Kinect;

namespace HelloKinect.Sensor
{
    public class KinectSensorService : ISensorService
    {
        KinectSensor sensor;
        Body[] bodies = new Body[6];

        public event EventHandler<IFrame> NewFrame;

        public KinectSensorService()
        {
            sensor = KinectSensor.GetDefault();
            FrameDescription fd = sensor.ColorFrameSource.FrameDescription;

            FrameDescription irFd = sensor.InfraredFrameSource.FrameDescription;
            MultiSourceFrameReader msfr = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.Color | FrameSourceTypes.Infrared);
            msfr.MultiSourceFrameArrived += FrameArrived;

            sensor.Open();
        }

        private void RaiseNewFrame(IDisposable frame, object data)
        {
            if (NewFrame != null)
            {
                NewFrame(this, new KinectFrame(frame, data));
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
                        RaiseNewFrame(colourFrame, sensor.ColorFrameSource.FrameDescription);
                        return;
                    }
                }

                using (BodyFrame bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
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

                        RaiseNewFrame(bodyFrame, sensor.CoordinateMapper.MapCameraPointToDepthSpace(headJoint.Position));
                        return;
                    }
                }

                using (InfraredFrame irFrame = multiSourceFrame.InfraredFrameReference.AcquireFrame())
                {
                    if (irFrame != null)
                    {
                        RaiseNewFrame(irFrame, sensor.InfraredFrameSource.FrameDescription);
                        return;
                    }
                }
            }
        }
    }
}
