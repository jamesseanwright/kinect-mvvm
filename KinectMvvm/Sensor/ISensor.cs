using KinectMvvm.Head;
using System;
using System.Collections.Generic;

namespace KinectMvvm.Sensor
{
    public interface ISensor
    {
        IObservable<byte[]> OnColourFrame { get; }
        IObservable<byte[]> OnInfraredFrame { get; }
        IObservable<List<HeadModel>> OnHeadFrame { get; }
    }
}