using GameCore.CustomExtensions.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class ScaleTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]
        
        [SerializeField] private bool uniformScale = true;

        [AllowNesting, HideIf("uniformScale")]
        [SerializeField] private Vector3 targetScale;

        [AllowNesting, ShowIf("uniformScale")]
        [SerializeField] private float targetUniformScale;

        public Vector3 GetTarget() 
            => (!uniformScale) ? targetScale : Vector3.one * targetUniformScale;
    }
}