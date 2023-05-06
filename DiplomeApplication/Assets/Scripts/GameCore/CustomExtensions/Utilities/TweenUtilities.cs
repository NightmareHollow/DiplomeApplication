using System;
using DG.Tweening;
using UnityEngine;

namespace GameCore.CustomExtensions.Utilities
{
	public static class TweenUtilities
	{
		public static Tween StartTweenFromPercentage(this Tween tween, float percentage)
		{
			if (tween == null)
				return null;
			
			percentage = Mathf.Clamp01(percentage);
			tween.fullPosition = percentage * tween.Duration();
			
			return tween;
		}

		public static Tween PlayTweenBeforePercentage(this Tween tween, float beforePercentage, 
			Action onCompleteAction, bool autoKill = true)
		{
			if (tween == null)
				return null;

			float clampedPercentage = Mathf.Clamp01(beforePercentage);
			if (clampedPercentage.Equals(1f))
			{
				onCompleteAction?.Invoke();
				return tween;
			}
			
			float clampedAnimPosition = clampedPercentage * tween.Duration();

			TweenCallback onUpdateCallback = () =>
			{
				float fullClampedPosition = tween.fullPosition % tween.Duration();
				Debug.Log(fullClampedPosition / tween.Duration());
				if (fullClampedPosition >= clampedAnimPosition)
				{
					if (autoKill)
						tween.Kill();
					
					onCompleteAction?.Invoke();
				}
			};

			tween.OnUpdate(onUpdateCallback);
			return tween;
		}
		
		public static Tween PlayTweenBeforePercentage(this Tween tween, float beforePercentage, 
			Action<float> onCompleteAction, bool autoKill = true)
		{
			if (tween == null)
				return null;

			float clampedPercentage = Mathf.Clamp01(beforePercentage);
			if (clampedPercentage.Equals(1f))
			{
				onCompleteAction?.Invoke(1f);
				return tween;
			}

			float tweenDuration = tween.Duration();
			float clampedAnimPosition = clampedPercentage * tweenDuration;

			TweenCallback onUpdateCallback = () =>
			{
				float fullPosition = tween.fullPosition;
				float fullClampedPosition = fullPosition % tweenDuration;
				if (fullClampedPosition >= clampedAnimPosition)
				{
					if (autoKill)
						tween.Kill();

					float percentage = Mathf.Clamp01(fullPosition / tweenDuration);
					onCompleteAction?.Invoke(percentage);
				}
			};

			tween.OnUpdate(onUpdateCallback);
			return tween;
		}
		
		public static Tween PlayTweenBeforeCondition(this Tween tween, Func<bool> condition,
			Action onCompleteAction, bool autoKill = true)
		{
			if (tween == null || condition == null)
				return tween;
			
			TweenCallback onUpdateCallback = () =>
			{
				if (!condition.Invoke())
				{
					if (autoKill)
						tween.Kill();
					
					onCompleteAction?.Invoke();
				}
			};
			
			tween.OnUpdate(onUpdateCallback);
			return tween;
		}
		
		
	}
}