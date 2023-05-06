using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.CustomExtensions.TweenPresets.Infrastructure.TweenPreset;
using GameCore.CustomExtensions.TweenPresets.TargetUnitsCollection;
using GameCore.CustomExtensions.Utilities;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.TweenAnimPresetCollection
{
    public class MoveAnimationPlayer : TweenAnimationPreset<Transform, MoveTargetTweenUnit>
    {
        private Vector3 initialPosition;
        private Vector3 endValuePosition;

        private TweenUnit BaseTweenUnit 
            => targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;

            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);
            
            Vector3 targetValue = (isInitialRelation) ? initialPosition : endValuePosition;
            if (targetTweenUnit.UseLocalPos)
            {
                targetValue = tweenTarget.ConvertLocalToWorldPos(targetValue);
            }

            Tween targetTween = tweenTarget.DOMove(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;

            Vector3 currentTargetPos = (targetTweenUnit.UseLocalPos) ? 
                tweenTarget.localPosition : tweenTarget.position;
            
            initialPosition = (relationIsTo) ? currentTargetPos : targetTweenUnit.GetTarget();
            endValuePosition = (relationIsTo) ? targetTweenUnit.GetTarget() : currentTargetPos;

            SetTargetPositionRelative(initialPosition);
        }

        protected override void ApplyInitialTargetValues()
        {
            SetTargetPositionRelative(initialPosition);
        }

        private void SetTargetPositionRelative(Vector3 position)
        {
            if (targetTweenUnit.UseLocalPos)
            {
                tweenTarget.localPosition = position;
            }
            else
            {
                tweenTarget.position = position;
            }
        }
    }
}