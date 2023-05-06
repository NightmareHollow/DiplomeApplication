using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.CustomExtensions.TweenPresets.Infrastructure.TweenPreset;
using GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TweenAnimPresetCollection
{
    public class RectMoveAnimationPlayer : TweenAnimationPreset<RectTransform, AnchoredMoveTargetTweenUnit>
    {
        private Vector2 initialAnchoredPosition;
        private Vector2 endValueAnchoredPosition;

        private TweenUnit BaseTweenUnit 
            => targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;

            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);
            
            Vector2 targetValue = (isInitialRelation) ? initialAnchoredPosition : endValueAnchoredPosition;

            Tween targetTween = tweenTarget.DOAnchorPos(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;

            Vector2 currentTargetAnchoredPos = tweenTarget.anchoredPosition;
            
            initialAnchoredPosition = (relationIsTo) ? currentTargetAnchoredPos : targetTweenUnit.GetTarget();
            endValueAnchoredPosition = (relationIsTo) ? targetTweenUnit.GetTarget() : currentTargetAnchoredPos;

            tweenTarget.anchoredPosition = initialAnchoredPosition;
        }

        protected override void ApplyInitialTargetValues()
        {
            tweenTarget.anchoredPosition = initialAnchoredPosition;
        }
    }
}