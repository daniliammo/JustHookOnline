using UnityEngine;

namespace UI
{
	public class ButtonSounds : MonoBehaviour
	{

		public AudioSource buttonFX;
		
		public AudioClip hoverSound1;
		public AudioClip hoverSound2;
		public AudioClip hoverSound3;


		public void PlayHoverSound1()
		{
			buttonFX.PlayOneShot(hoverSound1);
		}
		
		public void PlayHoverSound2()
		{
			buttonFX.PlayOneShot(hoverSound2);
		}
		
		public void PlayHoverSound3()
		{
			buttonFX.PlayOneShot(hoverSound3);
		}
		
	}
}