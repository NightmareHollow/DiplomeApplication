using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.CustomExtensions.Utilities;
using GameCore.TweenPresets.Infrastructure.TweenPreset;
using GameCore.TweenPresets.TargetUnitsCollection;
using UnityEngine.UI;

namespace GameCore.TweenPresets.TweenAnimPresetCollection
{
    public class FadeAnimationPlayer : TweenAnimationPreset<Graphic, PercentTargetTweenUnit>
    {
        private float initialFadePercent;
        private float endValueFadePercent;

        private TweenUnit BaseTweenUnit 
            => _targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;

            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);
            
            float targetValue = (isInitialRelation) ? initialFadePercent : endValueFadePercent;

            Tween targetTween = _tweenTarget.DOFade(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;

            float currentTargetAnchoredPos = _tweenTarget.color.a;
            
            initialFadePercent = (relationIsTo) ? currentTargetAnchoredPos : _targetTweenUnit.GetTarget();
            endValueFadePercent = (relationIsTo) ? _targetTweenUnit.GetTarget() : currentTargetAnchoredPos;

            _tweenTarget.SetTransparency(initialFadePercent);
        }

        protected override void ApplyInitialTargetValues()
        {
            _tweenTarget.SetTransparency(initialFadePercent);
        }
    }
}