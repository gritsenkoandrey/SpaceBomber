using System;
using System.Collections.Generic;


namespace Assets.Scripts.ServiceLocators
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _serviceConteiner = new Dictionary<Type, object>();

        public static void SetService<T>(T value) where T : class
        {
            var typeValue = value.GetType();
            if (!_serviceConteiner.ContainsKey(typeValue))
            {
                _serviceConteiner[typeValue] = value;
            }
        }

        public static T Resolve<T>()
        {
            return (T)_serviceConteiner[typeof(T)];
        }

        public static void Cleanup()
        {
            _serviceConteiner.Clear();
        }
    }
}