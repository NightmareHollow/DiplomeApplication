using UnityEngine;
using UnityEngine.UI;

namespace GameCore.SceneManagement.SceneTransition
{
	public class ScreenFadeImage : MonoBehaviour
	{
		[SerializeField] private Image fadeImage;

		public Image FadeImage => fadeImage;
	}
}