using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WindowsPreview.Kinect;
using KinectMvvm.Head;

namespace KinectMvvm.Sensor
{
    public class Kinect : ISensor
    {
        KinectSensor sensor;
        MultiSourceFrameReader frameReader;
        Subject<byte[]> onColourFrame;
        Subject<byte[]> onInfraredFrame;
        Subject<List<HeadModel>> onHeadFrame;

        public IObservable<byte[]> OnColourFrame
        {
            get { return this.onColourFrame.AsObservable(); }
        }

        public IObservable<byte[]> OnInfraredFrame
        {
            get { return this.onInfraredFrame.AsObservable(); }
        }

        public IObservable<List<HeadModel>> OnHeadFrame
        {
            get { return this.onHeadFrame.AsObservable(); }
        }

        public Kinect()
        {
            this.onColourFrame = new Subject<byte[]>();
            this.onInfraredFrame = new Subject<byte[]>();
            this.onHeadFrame = new Subject<List<HeadModel>>();

            this.sensor = KinectSensor.GetDefault();
            this.frameReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
            this.frameReader.MultiSourceFrameArrived += FrameArrived;
            this.sensor.Open();
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
                        this.onColourFrame.OnNext(colourData);
                    }
                }

                using (BodyFrame bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame())
                {
                    if (bodyFrame != null)
                    {
                        Body[] bodies = new Body[6];
                        List<HeadModel> heads = new List<HeadModel>();

                        bodyFrame.GetAndRefreshBodyData(bodies);

                        foreach (Body body in bodies)
                        {
                            Joint headJoint = body.Joints[JointType.Head];

                            if (headJoint.TrackingState == TrackingState.Tracked)
                            {
                                ColorSpacePoint spacePoint = sensor.CoordinateMapper.MapCameraPointToColorSpace(headJoint.Position);
                                heads.Add(new HeadModel(spacePoint.X, spacePoint.Y, headJoint.Position.Z));
                            }
                        }

                        this.onHeadFrame.OnNext(heads);
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

                        this.onInfraredFrame.OnNext(irDataConverted);
                    }
                }
            }
        }
    }
}
