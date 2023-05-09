using GameCore.CustomExtensions.DoTweenExtensions.TweenPlayersSystem;
using GameCore.ScreenManagement.ScreensControl;
using UnityEngine;

namespace UI.MainMenu.WelcomeScreen
{
    public class WelcomeScreenArrowControl : ScreenListenerBase
    {
        [SerializeField] private TweenPlayer _animationTween;
        
        public override void ReactOnShown()
        {
            _animationTween.Animate(true, null, true);
        }

        public override void ReactOnHidden()
        {
            ToDefaultState();
        }

        private void ToDefaultState()
        {
            _animationTween.KillPlayer(false);
        }

        private void OnDisable()
        {
            ToDefaultState();
        }
    }
}