using GameCore.CustomExtensions.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class PercentTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]

        [Range(0f, 1f), SerializeField] private float targetPercent;

        public float GetTarget() => targetPercent;
    }
}