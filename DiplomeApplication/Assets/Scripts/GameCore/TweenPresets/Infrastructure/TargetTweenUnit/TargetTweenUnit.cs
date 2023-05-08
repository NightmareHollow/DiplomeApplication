using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using UnityEngine;

namespace GameCore.TweenPresets.Infrastructure.TargetTweenUnit
{
    [System.Serializable]
    public abstract class TargetTweenUnit
    {
        [SerializeField] private TweenUnit _tweenUnit;

        public TweenUnit AttachedTweenUnit => _tweenUnit;
    }
}