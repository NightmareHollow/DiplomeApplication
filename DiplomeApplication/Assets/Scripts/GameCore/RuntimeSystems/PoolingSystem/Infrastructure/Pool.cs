using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RuntimeSystems.PoolingSystem.Infrastructure
{
    public struct Pool
    {
        public Stack<Component> Objects { get; }
        public Transform PoolParent { get; }

        public Pool(Transform poolsParent, string objectName)
        {
            PoolParent = new GameObject(objectName).transform;
            PoolParent.SetParent(poolsParent);

            Objects = new Stack<Component>();
        }
    }
}