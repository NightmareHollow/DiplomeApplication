using System;
using System.Collections;
using DG.Tweening;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenPlayersSystem;
using GameCore.CustomExtensions.Utilities;
using UnityEngine;

namespace GameCore.CustomExtensions.TweenPresets.Infrastructure.TweenPreset
{
    public abstract class TweenAnimationPreset<TTarget, TUnit> : TweenPlayer 
        where TTarget : Component 
        where TUnit : TargetTweenUnit.TargetTweenUnit
    {
        [Header("References")]
        
        [SerializeField] protected TTarget tweenTarget;

        [Header("Values")]
        
        [SerializeField] protected TUnit targetTweenUnit;

        //possibly move into the superClass to check it outside. 
        [SerializeField] private bool ignoreTimeScale;

        private Action completeAction;
        private Tween activeTween;

        private Coroutine playTweenRoutine;
        
        private bool initialized;

        private void Start()
        {
            TryInitializeAnimationPreset();
        }

        public sealed override float Duration(bool playForward = true) 
            => targetTweenUnit.AttachedTweenUnit.FullTweenDuration();

        public override void Animate(bool playForward, Action onCompleteAction = null, bool resetIncluded = false)
        {
            TryInitializeAnimationPreset();

            if (resetIncluded)
            {
                ToDefaultState();
            }

            completeAction = onCompleteAction;
            HandleAnimateTweenProcess(playForward);
        }

        public override void KillPlayer(bool onComplete)
        {
            ToDefaultState();

            if (onComplete)
            {
                HandleTweenAnimationComplete();
            }
        }

        private void TryInitializeAnimationPreset()
        {
            if (initialized)
                return;

            SaveInitialTargetValues();
            
            initialized = true;
        }

        protected abstract Tween HandlePlayAnimationTween(bool playForward);
        protected abstract void SaveInitialTargetValues();
        protected abstract void ApplyInitialTargetValues();
        
        private void HandleAnimateTweenProcess(bool playForward)
        {
            DeactivatePlayRoutine();
            playTweenRoutine = StartCoroutine(PlayTweenRoutine(playForward));
        }

        private IEnumerator PlayTweenRoutine(bool playForward)
        {
            activeTween = HandlePlayAnimationTween(playForward).SetUpdate(ignoreTimeScale);
            yield return activeTween.WaitForCompletion();
            
            HandleTweenAnimationComplete();
            playTweenRoutine = null;
        }

        private void HandleTweenAnimationComplete()
        {
            activeTween = null;
            
            Action tempCallback = completeAction;
            completeAction = null;
            tempCallback?.Invoke();
        }

        private void DeactivatePlayRoutine()
            => this.DeactivateCoroutine(ref playTweenRoutine);

        private void ToDefaultAnimationState()
        {
            if (!initialized)
                return;
            
            ApplyInitialTargetValues();
        }

        private void ToDefaultState()
        {
            activeTween?.Kill();

            DeactivatePlayRoutine();
        }

        private void OnDisable()
        {
            ToDefaultAnimationState();
        }

        private void OnDestroy()
        {
            ToDefaultState();
        }

#if UNITY_EDITOR

        private void Reset()
        {
            if (TryGetComponent(out TTarget targetComponent))
            {
                tweenTarget = targetComponent;
                
                EditorExtensions.MarkObjectDirty(this);
                EditorExtensions.SaveAndRefreshAssets();
            }
        }

#endif
    }
}