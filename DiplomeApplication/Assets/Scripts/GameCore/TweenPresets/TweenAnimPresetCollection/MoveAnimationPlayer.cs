using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;
using GameCore.CustomExtensions.Utilities;
using GameCore.TweenPresets.Infrastructure.TweenPreset;
using GameCore.TweenPresets.TargetUnitsCollection;
using UnityEngine;

namespace GameCore.TweenPresets.TweenAnimPresetCollection
{
    public class MoveAnimationPlayer : TweenAnimationPreset<Transform, MoveTargetTweenUnit>
    {
        private Vector3 initialPosition;
        private Vector3 endValuePosition;

        private TweenUnit BaseTweenUnit 
            => _targetTweenUnit.AttachedTweenUnit;
        
        protected override Tween HandlePlayAnimationTween(bool playForward)
        {
            TweenTargetRelationType currentTargetRelationType = CustomTweenExtensions.
                FindAnimTargetRelation(BaseTweenUnit.TargetRelationType, playForward);
            bool isInitialRelation = currentTargetRelationType == BaseTweenUnit.TargetRelationType;

            Ease animationEase = (playForward) ? BaseTweenUnit.TweenEase :
                CustomTweenExtensions.FindOppositeEase(BaseTweenUnit.TweenEase);
            
            Vector3 targetValue = (isInitialRelation) ? initialPosition : endValuePosition;
            if (_targetTweenUnit.UseLocalPos)
            {
                targetValue = _tweenTarget.ConvertLocalToWorldPos(targetValue);
            }

            Tween targetTween = _tweenTarget.DOMove(targetValue, BaseTweenUnit.Duration).
                SetDelay(BaseTweenUnit.Delay).SetEase(animationEase).
                SetLoops(BaseTweenUnit.Loops, BaseTweenUnit.TweenLoopType);

            return targetTween;
        }

        protected override void SaveInitialTargetValues()
        {
            TweenTargetRelationType targetRelationType = BaseTweenUnit.TargetRelationType;
            bool relationIsTo = targetRelationType == TweenTargetRelationType.To;

            Vector3 currentTargetPos = (_targetTweenUnit.UseLocalPos) ? 
                _tweenTarget.localPosition : _tweenTarget.position;
            
            initialPosition = (relationIsTo) ? currentTargetPos : _targetTweenUnit.GetTarget();
            endValuePosition = (relationIsTo) ? _targetTweenUnit.GetTarget() : currentTargetPos;

            SetTargetPositionRelative(initialPosition);
        }

        protected override void ApplyInitialTargetValues()
        {
            SetTargetPositionRelative(initialPosition);
        }

        private void SetTargetPositionRelative(Vector3 position)
        {
            if (_targetTweenUnit.UseLocalPos)
            {
                _tweenTarget.localPosition = position;
            }
            else
            {
                _tweenTarget.position = position;
            }
        }
    }
}