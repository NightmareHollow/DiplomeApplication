using GameCore.TweenPresets.Infrastructure.TargetTweenUnit;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.TweenPresets.TargetUnitsCollection
{
    [System.Serializable]
    public class ScaleTargetTweenUnit : TargetTweenUnit
    {
        [HorizontalLine(color: EColor.Green)]
        
        [SerializeField] private bool _uniformScale = true;

        [AllowNesting, HideIf("uniformScale")]
        [SerializeField] private Vector3 _targetScale;

        [AllowNesting, ShowIf("uniformScale")]
        [SerializeField] private float _targetUniformScale;

        public Vector3 GetTarget() 
            => (!_uniformScale) ? _targetScale : Vector3.one * _targetUniformScale;
    }
}