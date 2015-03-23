using System;
using Windows.UI.Xaml.Navigation;

namespace KinectMvvm.Framework
{
    public interface INavigator
    {
        void Navigate(Type viewModel);
    }
}
