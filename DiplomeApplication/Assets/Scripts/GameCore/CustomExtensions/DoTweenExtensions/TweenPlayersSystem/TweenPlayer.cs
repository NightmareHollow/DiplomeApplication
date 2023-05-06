using System;
using GameCore.CustomExtensions.DoTweenExtensions.Infrastructure.DoTweenConfiguration;
using UnityEngine;

namespace GameCore.CustomExtensions.DoTweenExtensions.TweenPlayersSystem
{
	public abstract class TweenPlayer : MonoBehaviour
	{
		protected TweenStartConfiguration tweenAnimationsStartConfiguration = TweenStartConfiguration.DoNothing;
		
		public abstract float Duration(bool playForward = true);
		
		public abstract void Animate(bool playForward, Action onCompleteAction = null, bool resetIncluded = false);

		public abstract void KillPlayer(bool onComplete);

		public virtual void ResetAnimation() { }

		public void ResetTweenStartConfiguration(TweenStartConfiguration newTweenStartConfiguration)
		{
			tweenAnimationsStartConfiguration = newTweenStartConfiguration;
		}

#if UNITY_EDITOR
		public virtual void UpdateAnimationsIds() { }
#endif
	}
}