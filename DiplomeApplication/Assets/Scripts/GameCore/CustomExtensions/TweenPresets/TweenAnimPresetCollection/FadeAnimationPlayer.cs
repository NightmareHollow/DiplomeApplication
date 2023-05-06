using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.CustomExtensions.TweenPresets.Infrastructure.TweenPreset;
using GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection;
using GameCore.CustomExtensions.Utilities;
using UnityEngine.UI;

namespace GameCore.CustomExtensions.TweenPresets.TweenAnimPresetCollection
{
    public class FadeAnimationPlayer : TweenAnimationPreset<Graphic, PercentTargetTweenUnit>
    {
        private float initialFadePercent;
        private float endValueFadePercent;

        private TweenUnit BaseTweenUnit 
            => targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;

            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);
            
            float targetValue = (isInitialRelation) ? initialFadePercent : endValueFadePercent;

            Tween targetTween = tweenTarget.DOFade(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;

            float currentTargetAnchoredPos = tweenTarget.color.a;
            
            initialFadePercent = (relationIsTo) ? currentTargetAnchoredPos : targetTweenUnit.GetTarget();
            endValueFadePercent = (relationIsTo) ? targetTweenUnit.GetTarget() : currentTargetAnchoredPos;

            tweenTarget.SetTransparency(initialFadePercent);
        }

        protected override void ApplyInitialTargetValues()
        {
            tweenTarget.SetTransparency(initialFadePercent);
        }
    }
}