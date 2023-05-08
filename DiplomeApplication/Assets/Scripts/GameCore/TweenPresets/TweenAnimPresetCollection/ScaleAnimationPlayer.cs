using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.TweenPresets.Infrastructure.TweenPreset;
using GameCore.TweenPresets.TargetUnitsCollection;
using UnityEngine;

namespace GameCore.TweenPresets.TweenAnimPresetCollection
{
    public class ScaleAnimationPlayer : TweenAnimationPreset<Transform, ScaleTargetTweenUnit>
    {
        private Vector3 initialLocalScale;
        private Vector3 endValueLocalScale;

        private TweenUnit BaseTweenUnit 
            => _targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;
            
            Vector3 targetValue = (isInitialRelation) ? initialLocalScale : endValueLocalScale;
            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);

            Tween targetTween = _tweenTarget.DOScale(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;
            
            initialLocalScale = (relationIsTo) ? _tweenTarget.localScale : _targetTweenUnit.GetTarget();
            endValueLocalScale = (relationIsTo) ? _targetTweenUnit.GetTarget() : _tweenTarget.localScale;

            _tweenTarget.localScale = initialLocalScale;
        }

        protected override void ApplyInitialTargetValues()
        {
            _tweenTarget.localScale = initialLocalScale;
        }
    }
}