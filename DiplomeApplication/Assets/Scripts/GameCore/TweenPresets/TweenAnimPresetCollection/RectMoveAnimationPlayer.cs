using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.TweenPresets.Infrastructure.TweenPreset;
using GameCore.TweenPresets.TargetUnitsCollection;
using UnityEngine;

namespace GameCore.TweenPresets.TweenAnimPresetCollection
{
    public class RectMoveAnimationPlayer : TweenAnimationPreset<RectTransform, AnchoredMoveTargetTweenUnit>
    {
        private Vector2 initialAnchoredPosition;
        private Vector2 endValueAnchoredPosition;

        private TweenUnit BaseTweenUnit 
            => _targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;

            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);
            
            Vector2 targetValue = (isInitialRelation) ? initialAnchoredPosition : endValueAnchoredPosition;

            Tween targetTween = _tweenTarget.DOAnchorPos(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;

            Vector2 currentTargetAnchoredPos = _tweenTarget.anchoredPosition;
            
            initialAnchoredPosition = (relationIsTo) ? currentTargetAnchoredPos : _targetTweenUnit.GetTarget();
            endValueAnchoredPosition = (relationIsTo) ? _targetTweenUnit.GetTarget() : currentTargetAnchoredPos;

            _tweenTarget.anchoredPosition = initialAnchoredPosition;
        }

        protected override void ApplyInitialTargetValues()
        {
            _tweenTarget.anchoredPosition = initialAnchoredPosition;
        }
    }
}