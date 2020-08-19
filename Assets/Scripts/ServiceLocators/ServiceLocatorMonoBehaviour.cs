using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Object = UnityEngine.Object;


namespace Assets.Scripts.ServiceLocators
{
    public static class ServiceLocatorMonoBehaviour
    {
        private static Dictionary<Type, object> _serviceContainer = new Dictionary<Type, object>();

        public static T GetService<T>(bool createObjectIfNotFound = true) where T: Object
        {
            if (!_serviceContainer.ContainsKey(typeof(T)))
            {
                return FindService<T>(createObjectIfNotFound);
            }

            var service = (T)_serviceContainer[typeof(T)];
            if (service != null)
            {
                return service;
            }
            _serviceContainer.Remove(typeof(T));

            return FindService<T>(createObjectIfNotFound);
        }

        private static T FindService<T>(bool createObjectIsNotFound = true) where T : Object
        {
            T type = Object.FindObjectOfType<T>();
            if (type != null)
            {
                _serviceContainer.Add(typeof(T), type);
            }
            else if (createObjectIsNotFound)
            {
                var go = new GameObject(typeof(T).Name, typeof(T));
                _serviceContainer.Add(typeof(T), go.GetComponent<T>());
            }

            return (T)_serviceContainer[typeof(T)];
        }

        public static void Cleanup()
        {
            var objects = _serviceContainer.Values.ToList();
            foreach (Object obj in objects)
            {
                Object.Destroy(obj);
            }

            _serviceContainer.Clear();
        }
    }
}