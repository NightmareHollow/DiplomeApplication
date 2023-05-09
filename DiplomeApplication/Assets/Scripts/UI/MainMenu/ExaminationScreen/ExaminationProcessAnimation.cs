using DG.Tweening;
using GameCore.ScreenManagement.ScreensControl;
using UnityEngine;

namespace UI.MainMenu.ExaminationScreen
{
    public class ExaminationProcessAnimation : ScreenListenerBase
    {
        [Header("References")]

        [SerializeField] private RectTransform _processRect;

        [Header("Values")]

        [SerializeField] private float _oneCycleTime = 1.2f;

        private Tween animationTween;

        public override void ReactOnShow()
        {
            AnimateProcess();
        }

        public override void ReactOnHide()
        {
            ToDefaultState();
        }

        private void AnimateProcess()
        {
            ToDefaultState();
            animationTween = _processRect.DORotate(Vector3.forward * -359.99f, _oneCycleTime, 
                RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.InOutCubic);
        }

        private void ToDefaultState()
        {
            animationTween.Kill();
            _processRect.localEulerAngles = Vector3.zero;
        }

        private void OnDisable()
        {
            ToDefaultState();
        }
    }
}