using System;
using System.Collections.Generic;

namespace GameCore.RuntimeSystems.PubSubSystem
{
    public static class PubSubService
    {
        private static readonly Dictionary<Type, Delegate> ParametrizedEvents = 
            new Dictionary<Type, Delegate>();

        private static readonly Dictionary<Type, Action> NonParametrizedEvents = 
            new Dictionary<Type, Action>();

        public delegate void ParametrizedAction<in T>(T data);

        public static void RegisterListener<T>(Action listener) where T : IObserverEvent
        {
            Type eventType = typeof(T);

            if (NonParametrizedEvents.ContainsKey(eventType))
            {
                NonParametrizedEvents[eventType] += listener;
            }
            else
            {
                NonParametrizedEvents.Add(eventType, listener);
            }
        }
        
        public static void RegisterListener<T>(ParametrizedAction<T> listener) where T : IObserverEvent
        {
            Type eventType = typeof(T);

            if (ParametrizedEvents.TryGetValue(eventType, out Delegate currentDelegate))
            {
                ParametrizedEvents[eventType] = Delegate.Combine(currentDelegate, listener);
            }
            else
            {
                ParametrizedEvents.Add(eventType, listener);
            }
        }
        
        public static void UnregisterListener<T>(Action listener) where T : IObserverEvent
        {
            Type eventType = typeof(T);

            if (!NonParametrizedEvents.ContainsKey(eventType))
                return;

            NonParametrizedEvents[eventType] -= listener;
            if (NonParametrizedEvents[eventType] == null)
            {
                NonParametrizedEvents.Remove(eventType);
            }
        }
        
        public static void UnregisterListener<T>(ParametrizedAction<T> listener) where T : IObserverEvent
        {
            Type eventType = typeof(T);

            if (!ParametrizedEvents.TryGetValue(eventType, out Delegate currentDelegate))
                return;

            ParametrizedEvents[eventType] = Delegate.Remove(currentDelegate, listener);
            if (ParametrizedEvents[eventType] == null)
            {
                ParametrizedEvents.Remove(eventType);
            }
        }

        public static void Publish<T>(in T sendEvent) where T : IObserverEvent
        {
            Type eventType = typeof(T);

            if (ParametrizedEvents.ContainsKey(eventType))
            {
                ((ParametrizedAction<T>) ParametrizedEvents[eventType])(sendEvent);
            }
            
            if (NonParametrizedEvents.ContainsKey(eventType))
            {
                NonParametrizedEvents[eventType]();
            }
        }
    }
}