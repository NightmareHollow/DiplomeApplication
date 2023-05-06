using GameCore.CustomExtensions.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class MoveTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private bool useLocalPos;

        [SerializeField] private Vector3 targetPos;

        public bool UseLocalPos => useLocalPos;
        
        public Vector3 GetTarget() => targetPos;
    }
}