using UnityEngine;

namespace GameCore.ScreenManagement.ScreensControl
{
	public abstract class ScreenListenerBase : MonoBehaviour
	{
		public virtual void ReactOnShow() { }
		public virtual void ReactOnShown() { }
		
		public virtual void ReactOnHide() { }
		public virtual void ReactOnHidden() { }
	}
}