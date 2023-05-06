using GameCore.CustomExtensions.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class AnchoredMoveTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private Vector2 targetPos;

        public Vector2 GetTarget() => targetPos;
    }
}