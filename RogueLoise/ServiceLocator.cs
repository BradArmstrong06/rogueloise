using System;
using System.ComponentModel.Design;

namespace RogueLoise
{
    public static class ServiceLocator
    {
        private static ServiceContainer _serviceManager = new ServiceContainer();

        public static void AddService(Type type, object service)
        {
            _serviceManager.AddService(type, service);
        }

        public static void AddService<T>(object service)
        {
            _serviceManager.AddService(typeof(T), service);
        }

        public static T GetService<T>()
        {
            return (T)_serviceManager.GetService(typeof (T));
        }
    }
}