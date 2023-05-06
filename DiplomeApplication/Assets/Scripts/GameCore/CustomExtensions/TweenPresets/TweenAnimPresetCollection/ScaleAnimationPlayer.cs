using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.CustomExtensions.TweenPresets.Infrastructure.TweenPreset;
using GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TweenAnimPresetCollection
{
    public class ScaleAnimationPlayer : TweenAnimationPreset<Transform, ScaleTargetTweenUnit>
    {
        private Vector3 initialLocalScale;
        private Vector3 endValueLocalScale;

        private TweenUnit BaseTweenUnit 
            => targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;
            
            Vector3 targetValue = (isInitialRelation) ? initialLocalScale : endValueLocalScale;
            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);

            Tween targetTween = tweenTarget.DOScale(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;
            
            initialLocalScale = (relationIsTo) ? tweenTarget.localScale : targetTweenUnit.GetTarget();
            endValueLocalScale = (relationIsTo) ? targetTweenUnit.GetTarget() : tweenTarget.localScale;

            tweenTarget.localScale = initialLocalScale;
        }

        protected override void ApplyInitialTargetValues()
        {
            tweenTarget.localScale = initialLocalScale;
        }
    }
}