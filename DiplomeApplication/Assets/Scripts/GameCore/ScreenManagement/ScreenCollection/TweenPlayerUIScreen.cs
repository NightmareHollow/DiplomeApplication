using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.DoTweenExtensions.TweenPlayersSystem;
using GameCore.CustomExtensions.Utilities;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.ScreenManagement.ScreenCollection
{
	public class TweenPlayerUIScreen : UIScreen
	{
		[Header("Tweens Players")]

		[SerializeField] private List<TweenPlayer> tweenPlayers = new List<TweenPlayer>();

		private Coroutine waitForAnimationRoutine;

		public List<TweenPlayer> TweenPlayers => tweenPlayers;

		public override void Show(Action onCompleteCallback = null)
		{
			if (IsAnimating)
				return;
			
			gameObject.SetActive(true);
			
			base.Show(onCompleteCallback);
			
			PlayTweenPlayers(true);
		}

		public override void Hide(Action onCompleteCallback = null)
		{
			if (IsAnimating)
				return;
			
			base.Hide(() =>
			{
				gameObject.SetActive(false);
				onCompleteCallback?.Invoke();
			});
			
			PlayTweenPlayers(false);
		}

		public override void Stop(bool onComplete)
		{
			foreach (TweenPlayer tweenPlayer in tweenPlayers)
			{
				tweenPlayer.KillPlayer(false);
			}
			
			ToDefaultAnimationState();
			
			base.Stop(onComplete);
		}
		
		private void PlayTweenPlayers(bool playForward, bool useReset = false)
		{
			foreach (TweenPlayer tweenPlayer in tweenPlayers)
			{
				tweenPlayer.Animate(playForward, null, useReset);
			}
			
			ActivateWaitForAnimations();
		}

		private void ActivateWaitForAnimations()
		{
			ToDefaultAnimationState();
			
			float maxDuration = GetMaxDuration();
			waitForAnimationRoutine = StartCoroutine(WaitForAnimationsRoutine(maxDuration));
		}

		private IEnumerator WaitForAnimationsRoutine(float duration)
		{
			yield return new WaitForSecondsRealtime(duration);
			
			CompleteAnimation(true);
			waitForAnimationRoutine = null;
		}

		private float GetMaxDuration()
		{
			float maxDuration = 0f;
			foreach (TweenPlayer tweenPlayer in tweenPlayers)
			{
				float duration = tweenPlayer.Duration();
				if (duration > maxDuration)
				{
					maxDuration = duration;
				}
			}
			
			return maxDuration;
		}

		private void ToDefaultAnimationState()
		{
			this.DeactivateCoroutine(ref waitForAnimationRoutine);
		}

#if UNITY_EDITOR

		[Button("(Re)Load Tween Players")]
		private void LoadTweenPlayers()
		{
			tweenPlayers.Clear();
			tweenPlayers.LoadComponentsOfTypeFromParent(transform);
			
			EditorExtensions.MarkObjectDirty(this);
			EditorExtensions.SaveAndRefreshAssets();
		}
		
#endif

	}
}