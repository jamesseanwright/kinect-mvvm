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
using KinectMvvm.Sensor;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KinectMvvm.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {

        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new Kinect());
        }
    }
}
