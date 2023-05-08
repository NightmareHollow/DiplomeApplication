using GameCore.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class AnchoredMoveTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private Vector2 _targetPos;

        public Vector2 GetTarget() => _targetPos;
    }
}