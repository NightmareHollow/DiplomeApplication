using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.Infrastructure.TargetTweenUnit
{
    [System.Serializable]
    public abstract class TargetTweenUnit
    {
        [SerializeField] private TweenUnit tweenUnit;

        public TweenUnit AttachedTweenUnit => tweenUnit;
    }
}