using System;
using System.Collections.Generic;
using GameCore.Services.ServiceStructure;

namespace GameCore.Services.Infrastructure
{
    public static class MonoBehaviourServicesContainer
    {
        private static readonly Dictionary<Type, IMonoBehaviourService> RegisteredServicesDict =
            new Dictionary<Type, IMonoBehaviourService>();

        public static bool ContainsService<T>() where T : IMonoBehaviourService
        {
            return RegisteredServicesDict.ContainsKey(typeof(T));
        }

        public static T GetService<T>() where T : IMonoBehaviourService
        {
            Type key = typeof(T);
            
            return (ContainsService<T>()) ? (T) RegisteredServicesDict[key] : default;
        }

        public static void AddService<T>(Type serviceType, T serviceToAdd) where T : IMonoBehaviourService
        {
            if (RegisteredServicesDict.ContainsKey(serviceType))
            {
                RegisteredServicesDict[serviceType] = serviceToAdd;
            }
            else
            {
                RegisteredServicesDict.Add(serviceType, serviceToAdd);
            }
        }
        
        public static void AddService<T>(T serviceToAdd) where T : IMonoBehaviourService
        {
            Type serviceType = serviceToAdd.GetType();
            
            if (RegisteredServicesDict.ContainsKey(serviceType))
            {
                RegisteredServicesDict[serviceType] = serviceToAdd;
            }
            else
            {
                RegisteredServicesDict.Add(serviceType, serviceToAdd);
            }
        }

        public static bool RemoveService<T>(T serviceToRemove) where T : IMonoBehaviourService
        {
            Type serviceType = typeof(T);
            bool result = RegisteredServicesDict.Remove(serviceType);
            return result;
        }
    }
}