using System;

namespace GameCore.ScreenManagement.ScreenCollection
{
	public class CommonUIScreen : UIScreen
	{
		public override void Show(Action onCompleteCallback = null)
		{
			if (IsAnimating)
				return;
			
			gameObject.SetActive(true);
			
			base.Show(onCompleteCallback);
			
			CompleteAnimation(true);
		}

		public override void Hide(Action onCompleteCallback = null)
		{
			if (IsAnimating)
				return;
			
			base.Hide(onCompleteCallback);
			
			CompleteAnimation(true);
			gameObject.SetActive(false);
		}
	}
}