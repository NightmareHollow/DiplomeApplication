using System;
using DG.Tweening;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.DoTweenExtensions.Extensions;
using GameCore.CustomExtensions.Utilities;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.CustomExtensions.UIAnimationsPresets.Button
{
    public class SmoothTapButtonAnim : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] private UnityEngine.UI.Button targetButton;

        [Header("Values")]

        [Min(0.01f), SerializeField] private float animDuration = 0.3f;
        
        [SerializeField] private Ease animationEase = Ease.InOutSine;

        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private bool autoPlay;
        [SerializeField] private bool autoChangeInteractable;

        private Vector3 initialScale;
        private bool initialInteractiveState;
        
        private Tween activePlayTween;
        
        private bool initialized;

        public UnityEngine.UI.Button TargetButton => targetButton;

        private void Start()
        {
            TryInitializeAnim();
            
            if (autoPlay)
            {
                targetButton.onClick.AddListener(InnerAnimateButton);
            }
        }

        private void TryInitializeAnim()
        {
            if (initialized)
                return;

            initialScale = targetButton.transform.localScale;
            initialInteractiveState = targetButton.interactable;
            
            initialized = true;
        }

        public void AnimateButton(Action completeInPartCallback = null, Action animCompleteCallback = null)
        {
            TryInitializeAnim();
            ToDefaultState();

            float halfDuration = animDuration / 2f;
            Vector3 targetScale = GetTargetScale();

            if (autoChangeInteractable)
                targetButton.interactable = false;

            activePlayTween = targetButton.transform.DOScale(targetScale, halfDuration).SetEase(animationEase).
                SetUpdate(true).OnComplete(() =>
                {
                    completeInPartCallback?.Invoke();
                    Ease outerAnimEase = CustomTweenExtensions.FindOppositeEase(animationEase);

                    activePlayTween = targetButton.transform.DOScale(initialScale, halfDuration).SetEase(outerAnimEase).
                        SetUpdate(true).OnComplete(() =>
                        {
                            animCompleteCallback?.Invoke();
                            
                            activePlayTween = null;
                            if (autoChangeInteractable)
                                targetButton.interactable = true;
                        });
                });
        }

        private void InnerAnimateButton()
            => AnimateButton();

        private Vector3 GetTargetScale()
        {
            float xScale = initialScale.x - 0.15f;
            xScale = CustomUtilities.ClampLowerValueLimit(xScale);
            
            float yScale = initialScale.x - 0.15f;
            yScale = CustomUtilities.ClampLowerValueLimit(yScale);
            
            float zScale = initialScale.x - 0.15f;
            zScale = CustomUtilities.ClampLowerValueLimit(zScale);

            return new Vector3(xScale, yScale, zScale);
        }

        private void ToDefaultState(bool deactivated = false)
        {
            activePlayTween?.Kill();

            if (!initialized) 
                return;
            
            targetButton.transform.localScale = initialScale;

            if (deactivated)
            {
                targetButton.interactable = initialInteractiveState;
            }
        }

        private void OnDisable()
        {
            ToDefaultState(true);
        }

        private void OnDestroy()
        {
            if (autoPlay)
            {
                targetButton.onClick.RemoveListener(InnerAnimateButton);
            }
        }

#if UNITY_EDITOR

        private void Reset()
        {
            if (TryGetComponent(out UnityEngine.UI.Button foundButton))
            {
                targetButton = foundButton;
                
                EditorExtensions.MarkObjectDirty(this);
                EditorExtensions.SaveAndRefreshAssets();
            }
        }

#endif
    }
}