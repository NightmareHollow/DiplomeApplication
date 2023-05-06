using UnityEngine;

namespace GameCore.RuntimeSystems.PoolingSystem.Infrastructure
{
    public interface IPoolComponent
    {
        public Component OriginalPrefab { get; set; } 
    }
}