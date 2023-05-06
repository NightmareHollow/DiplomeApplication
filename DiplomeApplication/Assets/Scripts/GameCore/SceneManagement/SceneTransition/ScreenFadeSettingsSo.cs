using GameCore.CustomExtensions.Collections;
using GameCore.Infrastructure;
using Multiplayer.Scripts.Game.SceneManagement.SceneTransition;
using UnityEngine;

namespace GameCore.SceneManagement.SceneTransition
{
	[CreateAssetMenu(fileName = "Screen Fade Settings", 
		menuName = GameCoreSoPaths.SoSceneTransition + "Screen Fade Settings")]
	public class ScreenFadeSettingsSo : ScriptableObject
	{
		[SerializeField] private GenericDictionary<ScreenFadeType, ScreenFadeAnimationUnit> screenFadeAnimationUnits;

		public ScreenFadeAnimationUnit this[ScreenFadeType screenFadeType]
			=> screenFadeAnimationUnits[screenFadeType];
	}
}