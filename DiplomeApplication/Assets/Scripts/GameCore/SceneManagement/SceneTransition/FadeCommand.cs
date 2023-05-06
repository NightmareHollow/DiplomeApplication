using System;
using Multiplayer.Scripts.Game.SceneManagement.SceneTransition;

namespace GameCore.SceneManagement.SceneTransition
{
	public class FadeCommand
	{
		public ScreenFadeType AttachedFadeType { get; }
		public Action OnCompleteAction { get; }

		public FadeCommand(ScreenFadeType attachedFadeType, Action onCompleteAction = null)
		{
			AttachedFadeType = attachedFadeType;
			OnCompleteAction = onCompleteAction;
		}
	}
}