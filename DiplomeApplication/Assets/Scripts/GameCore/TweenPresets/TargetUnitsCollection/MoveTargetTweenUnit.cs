using GameCore.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class MoveTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private bool _useLocalPos;

        [SerializeField] private Vector3 _targetPos;

        public bool UseLocalPos => _useLocalPos;
        
        public Vector3 GetTarget() => _targetPos;
    }
}