using UnityEngine;

namespace UI
{
	public class MobileUserInterfaceController : MonoBehaviour
	{

		public GameObject mobileUserInterface;
		
	
		private void Start()
		{
			mobileUserInterface.SetActive(RealInput.IsTouchSupported);
			Destroy(this);
		}
		
	}
}
