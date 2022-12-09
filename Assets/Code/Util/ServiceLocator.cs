using System;
using System.Collections.Generic;

namespace Code.Util
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();
        
        private Dictionary<Type, object> _registeredServices;

        public ServiceLocator()
        {
            _registeredServices = new Dictionary<Type, object>();
        }

        public T GetService<T>()
        {
            if (!_registeredServices.TryGetValue(typeof(T), out var service))
            {
                throw new Exception("Service not registerer!");
            }

            return (T)service;
        }
    }
}