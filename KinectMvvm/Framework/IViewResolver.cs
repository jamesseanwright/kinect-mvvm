using System;

namespace KinectMvvm.Framework
{
    public interface IViewResolver
    {
        Type GetFromViewModel(Type viewModel);
        void Register<TView, TViewModel>();
    }
}
