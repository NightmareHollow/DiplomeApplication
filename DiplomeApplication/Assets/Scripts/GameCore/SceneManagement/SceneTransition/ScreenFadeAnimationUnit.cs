using DG.Tweening;
using UnityEngine;

namespace Multiplayer.Scripts.Game.SceneManagement.SceneTransition
{
	[System.Serializable]
	public class ScreenFadeAnimationUnit
	{
		[Min(0f), SerializeField] private float animationTime = 1f;
		[SerializeField] private Ease animationEase = Ease.InSine;

		public float AnimationTime => animationTime;
		public Ease AnimationEase => animationEase;
	}
}