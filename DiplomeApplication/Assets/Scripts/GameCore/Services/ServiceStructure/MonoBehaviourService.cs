using UnityEngine;

namespace GameCore.Services.ServiceStructure
{
    public abstract class MonoBehaviourService : MonoBehaviour, IMonoBehaviourService
    {
        [Header("Service Base Values")]

        [SerializeField] private bool autoInjectService = true;

        public bool AutoInjectService => autoInjectService;
    }
}