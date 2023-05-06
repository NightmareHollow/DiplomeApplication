using UnityEngine;

namespace GameCore.RuntimeSystems.PoolingSystem.Settings
{
    [System.Serializable]
    public class PoolSettings
    {
        [SerializeField] private Component original;
        [SerializeField] private int amount;

        public Component Original
        {
            get => original;
            set => original = value;
        }

        public int Amount => amount;
    }
}