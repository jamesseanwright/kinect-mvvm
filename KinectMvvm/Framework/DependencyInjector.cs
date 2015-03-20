using System;
using System.Collections.Generic;

namespace KinectMvvm.Framework
{
    public class DependencyInjector
    {
        static DependencyInjector instance;

        public static void Initialise()
        {
            instance = new DependencyInjector();
        }

        Dictionary<Type, object> singletons;

        private DependencyInjector()
        {
            this.singletons = new Dictionary<Type, object>();
        }

        public void RegisterSingleton<T>(object instance)
        {
            this.singletons.Add(typeof (T), instance);
        }

        public T GetSingleton<T>()
        {
            object outRef = null;
            
            if (this.singletons.TryGetValue(typeof (T), out outRef))
            {
                return (T)outRef;
            }

            return default(T);
        }
    }
}
