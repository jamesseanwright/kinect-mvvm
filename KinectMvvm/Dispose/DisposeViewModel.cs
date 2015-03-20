using KinectMvvm.Framework;
using KinectMvvm.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectMvvm.Dispose
{
    public class DisposeViewModel : ViewModel
    {
        INavigator navigator;

        public DisposeViewModel(INavigator navigator)
        {
            this.navigator = navigator;
        }

        ActionCommand goBack;
        public ActionCommand GoBack
        {
            get
            {
                if (this.goBack == null)
                {
                    this.goBack = new ActionCommand(() =>
                    {
                        this.navigator.Navigate(typeof(MainView));
                    });
                }

                return this.goBack;
            }
        }
    }
}
