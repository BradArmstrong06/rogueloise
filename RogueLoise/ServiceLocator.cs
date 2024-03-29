﻿using System;
using System.ComponentModel.Design;

namespace RogueLoise
{
    public static class ServiceLocator
    {
        private static readonly ServiceContainer ServiceManager = new ServiceContainer();

        public static void AddService(Type type, object service)
        {
            ServiceManager.AddService(type, service);
        }

        public static void AddService<T>(object service)
        {
            ServiceManager.AddService(typeof (T), service);
        }

        public static T GetService<T>()
        {
            return (T) ServiceManager.GetService(typeof (T));
        }
    }
}