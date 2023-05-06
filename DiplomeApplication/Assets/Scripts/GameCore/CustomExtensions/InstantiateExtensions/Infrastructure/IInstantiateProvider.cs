using UnityEngine;

namespace GameCore.CustomExtensions.InstantiateExtensions.Infrastructure
{
    public interface IInstantiateProvider
    {
        public T Instantiate<T>(T original) where T : Object;
        public T Instantiate<T>(T original, Transform parent, bool worldPositionStays = false) where T : Object;
        public T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object;
        public T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object;
    }
}