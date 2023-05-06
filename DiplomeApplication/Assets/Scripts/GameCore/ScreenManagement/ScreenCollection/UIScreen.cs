using System;
using System.Collections.Generic;
using GameCore.CustomExtensions.CustomEditorExtensions;
using GameCore.CustomExtensions.Utilities;
using GameCore.ScreenManagement.ScreensControl;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.ScreenManagement.ScreenCollection
{
	public abstract class UIScreen : MonoBehaviour
	{
		[Header("Screen Listeners")]
		[SerializeField] private List<ScreenListenerBase> screenListeners = new List<ScreenListenerBase>();
		
		protected bool IsAnimating;
		private Action completeCallback;

		public virtual void Show(Action onCompleteCallback = null)
		{
			if (IsAnimating)
				return;
			
			IsAnimating = true;

			completeCallback = () =>
			{
				completeCallback = null;
				
				HandleSendEvent(ScreenEventType.Shown);
				onCompleteCallback?.Invoke();
			};
			
			HandleSendEvent(ScreenEventType.Show);
		}
		
		public virtual void Hide(Action onCompleteCallback = null)
		{
			if (IsAnimating)
				return;
			
			IsAnimating = true;

			completeCallback = () =>
			{
				completeCallback = null;
				
				HandleSendEvent(ScreenEventType.Hidden);
				onCompleteCallback?.Invoke();
			};
			
			HandleSendEvent(ScreenEventType.Hide);
		}

		public virtual void Stop(bool onComplete)
		{
			CompleteAnimation(onComplete);
		}

		protected void CompleteAnimation(bool considerOnCompleted)
		{
			if (considerOnCompleted)
			{
				completeCallback?.Invoke();
			}
			
			IsAnimating = false;
		}

		private void HandleSendEvent(ScreenEventType screenEventType)
		{
			switch (screenEventType)
			{
				case ScreenEventType.Show:
					SendToAllScreenListeners(listener => listener.ReactOnShow());
					break;
				
				case ScreenEventType.Shown:
					SendToAllScreenListeners(listener => listener.ReactOnShown());
					break;
				
				case ScreenEventType.Hide:
					SendToAllScreenListeners(listener => listener.ReactOnHide());
					break;
				
				case ScreenEventType.Hidden:
					SendToAllScreenListeners(listener => listener.ReactOnHidden());
					break;
				
				default:
					return;
			}
		}

		private void SendToAllScreenListeners(Action<ScreenListenerBase> listenerSendAction)
		{
			foreach (ScreenListenerBase listener in screenListeners)
			{
				listenerSendAction?.Invoke(listener);
			}
		}

#if UNITY_EDITOR

		[Button("(Re)Load Screen Listeners")]
		private void LoadListeners()
		{
			screenListeners.Clear();
			
			screenListeners.LoadComponentsOfTypeFromParent(transform);
			
			EditorExtensions.MarkObjectDirty(this);
			EditorExtensions.SaveAndRefreshAssets();
		}
		
#endif
		
	}
}