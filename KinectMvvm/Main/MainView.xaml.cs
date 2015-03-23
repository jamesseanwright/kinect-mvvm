using Windows.UI.Xaml.Controls;
using KinectMvvm.Sensor;
using KinectMvvm.Framework;

namespace KinectMvvm.Main
{
    public sealed partial class MainView : View
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = DependencyInjector.Instance.CreateInstance<MainViewModel>(typeof(ISensor), typeof(INavigator));
        }
    }
}
