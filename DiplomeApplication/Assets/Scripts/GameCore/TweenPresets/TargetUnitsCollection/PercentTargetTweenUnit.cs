using GameCore.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class PercentTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]

        [Range(0f, 1f), SerializeField] private float _targetPercent;

        public float GetTarget() => _targetPercent;
    }
}