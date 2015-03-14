using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KinectMvvm.Framework
{
    public class Navigator : INavigator
    {
        readonly Frame rootFrame;

        public Navigator()
        {
            this.rootFrame = (Frame)Window.Current.Content;
        }

        public void Navigate(Type viewType)
        {
            this.rootFrame.Navigate(viewType);
        }
    }
}
