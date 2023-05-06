using System;
using System.Collections.Generic;
using DG.Tweening;
using GameCore.CustomExtensions.Utilities;
using GameCore.Services.ServiceStructure;
using Multiplayer.Scripts.Game.SceneManagement.SceneTransition;
using UnityEngine;

namespace GameCore.SceneManagement.SceneTransition
{
	public class ScreenFader : InstantMonoBehaviourService
	{
		[Header("References")]

		[SerializeField] private ScreenFadeSettingsSo screenFadeSettings;
		[SerializeField] private ScreenFadeImage screenFadePrefab;

		private ScreenFadeImage screenFadeImage;
		
		private bool isAnimating;

		private readonly Queue<FadeCommand> fadeCommandsQueue = new Queue<FadeCommand>();

		private bool initialized;
		
		public float FadeAnimationTime(ScreenFadeType fadeType)
			=> screenFadeSettings[fadeType].AnimationTime;

		public override void InitializeService()
		{
			if (initialized)
				return;
			
			GenerateScreenFader();

			initialized = true;
		}

		public void FadeScreen(ScreenFadeType fadeType, Action onCompleteCallback = null)
		{
			FadeCommand newCommand = new FadeCommand(fadeType, () =>
			{
				onCompleteCallback?.Invoke();
				if (fadeCommandsQueue.Count > 0)
				{
					FadeCommand nextCommand = fadeCommandsQueue.Dequeue();
					CompleteFadeCommand(nextCommand);
				}
			});
			
			if (isAnimating)
			{
				fadeCommandsQueue.Enqueue(newCommand);
			}
			else
			{
				CompleteFadeCommand(newCommand);
			}
		}

		public void InstantFadeScreen(ScreenFadeType fadeType)
		{
			bool isFadeIn = fadeType == ScreenFadeType.FadeIn;
			KillFadeTween(true);

			ChangeActivationState(isFadeIn);
			ChangeTransparency((isFadeIn) ? 1f : 0f);
		}

		private void CompleteFadeCommand(FadeCommand fadeCommand)
		{
			isAnimating = true;
			
			FindFadeTransparencyValues(fadeCommand.AttachedFadeType, out float startValue, out float finishValue);
			ScreenFadeAnimationUnit animationUnit = screenFadeSettings[fadeCommand.AttachedFadeType];
			
			KillFadeTween();
			ChangeActivationState(true);
			ChangeTransparency(startValue);
			screenFadeImage.FadeImage.DOFade(finishValue, animationUnit.AnimationTime)
				.SetUpdate(true)
				.SetEase(animationUnit.AnimationEase).OnComplete(() =>
				{
					isAnimating = false;
					if (fadeCommand.AttachedFadeType == ScreenFadeType.FadeOut)
						ChangeActivationState(false);
					
					fadeCommand.OnCompleteAction?.Invoke();
				});
		}

		private void GenerateScreenFader()
		{
			screenFadeImage = Instantiate(screenFadePrefab);
			DontDestroyOnLoad(screenFadeImage);
			
			ChangeActivationState(false);
			ChangeTransparency(0f);
		}

		private void FindFadeTransparencyValues(ScreenFadeType fadeType, out float startValue, out float finishValue)
		{
			startValue = 0f;
			finishValue = 1f;
			
			if (fadeType == ScreenFadeType.FadeOut)
			{
				startValue = 1f;
				finishValue = 0f;
			}
		}

		private void ChangeActivationState(bool isActive)
		{
			screenFadeImage.gameObject.SetActive(isActive);
		}

		private void ChangeTransparency(float transparencyValue)
		{
			transparencyValue = Mathf.Clamp01(transparencyValue);
			screenFadeImage.FadeImage.SetTransparency(transparencyValue);
		}

		private void ToDefaultState()
		{
			KillFadeTween();
			
			fadeCommandsQueue.Clear();
		}
		
		private void KillFadeTween(bool complete = false)
		{
			if (!screenFadeImage)
				return;
			
			screenFadeImage.FadeImage.DOKill(complete);
		}

		private void OnDestroy()
		{
			ToDefaultState();
		}
	}
}