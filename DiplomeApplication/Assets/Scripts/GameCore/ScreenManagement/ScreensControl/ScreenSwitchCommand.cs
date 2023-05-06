using System;
using GameCore.ScreenManagement.ScreenCollection;

namespace GameCore.ScreenManagement.ScreensControl
{
	public class ScreenSwitchCommand
	{
		public UIScreen FromScreen { get; }
		public UIScreen ToScreen { get; }
		
		public Action OnCompleteFromAction { get; }
		public Action OnCompleteToAction { get; }
		
		public  ScreensTransitionType TransitionType { get; }
		
		public bool UseBlocker { get; }
		
		public ScreenSwitchCommand(UIScreen fromScreen, UIScreen toScreen,
			Action onCompleteFromAction, Action onCompleteToAction,
			ScreensTransitionType transitionType, bool useBlocker)
		{
			FromScreen = fromScreen;
			ToScreen = toScreen;
			
			OnCompleteFromAction = onCompleteFromAction;
			OnCompleteToAction = onCompleteToAction;
			
			TransitionType = transitionType;
			UseBlocker = useBlocker;
		}
	}
}