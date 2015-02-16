namespace KinectMvvm.Sensor
{
    public class ByteFrame
    {
        public ByteFrame(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }
    }
}